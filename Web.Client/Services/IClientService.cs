using Domain;
using Repository;

namespace Services;

public interface IClientService
{
    Task<Guid> CheckEmailOwner(string email);
    Task RegisterClient(Domain.Client client);
    Task<IList<Domain.Client>> GetClients(Domain.Client? filter);
    Task DeleteClient(Guid number);
    Task<Domain.Client> GetClientByNumber(Guid number);

}
