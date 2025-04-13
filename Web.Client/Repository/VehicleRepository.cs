﻿using System.Linq.Expressions;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private Context _context {  get; set; }
        public VehicleRepository(Context context)
        {
            _context = context;
            _context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
        }
        public async Task<Vehicle?> GetByNumber(Guid number)
        {
            var Vehicle = await _context.Vehicles.FirstOrDefaultAsync(c => c.Number.Equals(number.ToString()));
            return Vehicle;
        }
        public async Task<IList<Vehicle>> GetAll(Vehicle? filter)
        {
           if (filter != null &&
                !string.IsNullOrWhiteSpace(filter.Email) &&
                !string.IsNullOrWhiteSpace(filter.Name))
            {
                return await _context.Vehicles
                    .Where(c => c.Email.Equals(filter.Email, StringComparison.CurrentCultureIgnoreCase)
                        && c.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase))
                    .ToListAsync();
            }
            else if (filter != null &&
               !string.IsNullOrWhiteSpace(filter.Email))
            {
                return await _context.Vehicles
                    .Where(c => c.Email.Equals(filter.Email, StringComparison.CurrentCultureIgnoreCase))
                    .ToListAsync();
            }
            else if (filter != null &&
               !string.IsNullOrWhiteSpace(filter.Name))
            {
                return await _context.Vehicles
                    .Where(c => c.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase))
                    .ToListAsync();
            }
            else
                return await _context.Vehicles.ToListAsync();

        }
        public async Task Create(Vehicle Vehicle)
        {
            try
            {
                Vehicle.Id = ObjectId.GenerateNewId();
                Console.WriteLine("Repo GenerateNewId");
                await _context.Vehicles.AddAsync(Vehicle);
                Console.WriteLine("Repo Vehicles.Add");
                await _context.SaveChangesAsync();
                Console.WriteLine("Repo SaveChangesAsync");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Repo Finish Create");
        }

        public async Task Delete(Guid number)
        {
            try
            {
                var Vehicle = await _context.Vehicles.FirstOrDefaultAsync(c => c.Number.Equals(number.ToString()));
                _context.Vehicles.Remove(Vehicle);
                //TODO: Nao esta deixando excluir dois seguidos
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
