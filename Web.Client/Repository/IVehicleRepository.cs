
namespace Repository;

public interface IVehicleRepository
{
    Task<Vehicle?> GetByNumber(Guid id);
    Task<IList<Vehicle>> GetAll(Vehicle? filter);
    Task Create(Vehicle car);
    Task Delete(Guid number);
}

