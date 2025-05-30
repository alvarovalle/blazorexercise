using Domain;
using Repository;

namespace Services;

public class VehicleService(IVehicleRepository _vehicleRepository) : IVehicleService
{
    public async Task<IList<Domain.Vehicle>> GetVehicles(Domain.Vehicle? filter)
    {
        var vehiclesData = await _vehicleRepository.GetAll(new Repository.Vehicle()
        {
            Brand = filter?.Brand!,
            Model = filter?.Model!
        });

        var vehicles = vehiclesData.Select(vehicle => new Domain.Vehicle()
        {
            Number = new Guid(vehicle.Number),
            LicensePlate = vehicle.LicensePlate,
            Brand = vehicle.Brand,
            Model = vehicle.Model,
        }).ToList();

        return vehicles;
    }
    public async Task DeleteVehicle(Guid number)
    {
        await _vehicleRepository.Delete(number);
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
    public async Task RegisterNewVehicle(Domain.Vehicle vehicle)
    {

        vehicle.RegisterNewVehicle();
        var VehicleData = new Repository.Vehicle()
        {
            Number = vehicle.Number.ToString(),
            LicensePlate = vehicle.LicensePlate.ToString(),
            Brand = vehicle.Brand,
            Model = vehicle.Model,
        };
        await _vehicleRepository.Create(VehicleData);
    }

    public async Task<Domain.Vehicle> GetVehicleByNumber(Guid number)
    {
        var vehicleData = await _vehicleRepository.GetByNumber(number);
        var vehicle = new Domain.Vehicle()
        {
            Number = new Guid(vehicleData.Number),
            LicensePlate = vehicleData.LicensePlate.ToString(),
            Brand = vehicleData.Brand,
            Model = vehicleData.Model,
        };
        return vehicle;
    }
}
