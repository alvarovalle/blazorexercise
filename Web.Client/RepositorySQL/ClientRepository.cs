﻿using Microsoft.EntityFrameworkCore;

namespace RepositorySQL
{
    public class ClientRepository : IClientRepository
    {
        private Context _context {  get; set; }
        public ClientRepository(Context context)
        {
            _context = context;
            _context.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
        }
        public async Task<Client?> GetByNumber(Guid number)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Number.Equals(number.ToString()));
            return client;
        }
        public async Task<IList<Client>> GetAll(Client? filter)
        {
           if (filter != null &&
                !string.IsNullOrWhiteSpace(filter.Email) &&
                !string.IsNullOrWhiteSpace(filter.Name))
            {
                return await _context.Clients
                    .Where(c => c.Email.Equals(filter.Email, StringComparison.CurrentCultureIgnoreCase)
                        && c.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase))
                    .ToListAsync();
            }
            else if (filter != null &&
               !string.IsNullOrWhiteSpace(filter.Email))
            {
                return await _context.Clients
                    .Where(c => c.Email.Equals(filter.Email, StringComparison.CurrentCultureIgnoreCase))
                    .ToListAsync();
            }
            else if (filter != null &&
               !string.IsNullOrWhiteSpace(filter.Name))
            {
                return await _context.Clients
                    .Where(c => c.Name.Equals(filter.Name, StringComparison.CurrentCultureIgnoreCase))
                    .ToListAsync();
            }
            else
                return await _context.Clients.ToListAsync();

        }
        public async Task Create(Client client)
        {
            try
            {
                client.Id = Guid.NewGuid();
                Console.WriteLine("Repo GenerateNewId");
                await _context.Clients.AddAsync(client);
                Console.WriteLine("Repo Clients.Add");
                await _context.SaveChangesAsync();
                Console.WriteLine("Repo SaveChangesAsync");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Repo Finish Create");
        }

        public async Task DeleteClient(Guid number)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.Number.Equals(number.ToString()));
                _context.Clients.Remove(client);
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
