using Domain;
using Repository;

namespace Services;

public class VehicleService(IVehicleRepository _VehicleRepository) : IVehicleService
{
    public async Task<IList<Domain.Vehicle>> GetVehicles(Domain.Vehicle? filter)
    {
        var VehiclesData = await _VehicleRepository.GetAll(new Repository.Vehicle()
        {
            Brand = filter?.Brand!,
            Model = filter?.Model!
        });

        var Vehicles = VehiclesData.Select(Vehicle => new Domain.Vehicle()
        {
            Number = new Guid(Vehicle.Number),
            Brand = Vehicle.Brand,
            Model = Vehicle.Model,
        }).ToList();

        return Vehicles;
    }
    public async Task DeleteVehicle(Guid number)
    {
        await _VehicleRepository.Delete(number);
    }
    public async Task<string> CheckIfEmailExists(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) 
            return string.Empty;

        var Vehicles = await GetVehicles(new Domain.Vehicle() { Model = email });
        if (Vehicles.Any())
        {
            return "Email already in use";
        }

        return string.Empty;
    }
    public async Task RegisterNewVehicle(Domain.Vehicle Vehicle)
    {

        Vehicle.RegisterNewVehicle();
        var VehicleData = new Repository.Vehicle()
        {
            Number = Vehicle.Number.ToString(),
            Brand = Vehicle.Brand,
            Model = Vehicle.Model,
        };
        await _VehicleRepository.Create(VehicleData);
    }

    public async Task<Domain.Vehicle> GetVehicleByNumber(Guid number)
    {
        var VehicleData = await _VehicleRepository.GetByNumber(number);
        var Vehicle = new Domain.Vehicle()
        {
            Number = new Guid(VehicleData.Number),
            Brand = VehicleData.Brand,
            Model = VehicleData.Model,
        };
        return Vehicle;
    }
}
