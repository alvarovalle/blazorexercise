
namespace Repository;

public interface IClientRepository
{
    Task<Client?> GetByNumber(Guid id);
    Task<IList<Client>> GetAll(Client? filter);
    Task Create(Client car);
    Task Delete(Guid number);
}

