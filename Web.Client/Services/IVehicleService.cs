using Domain;
using Repository;

namespace Services;

public interface IVehicleService
{
    Task<string> CheckIfEmailExists(string email);
    Task RegisterNewVehicle(Domain.Vehicle Vehicle);
    Task<IList<Domain.Vehicle>> GetVehicles(Domain.Vehicle? filter);
    Task DeleteVehicle(Guid number);
    Task<Domain.Vehicle> GetVehicleByNumber(Guid number);

}
