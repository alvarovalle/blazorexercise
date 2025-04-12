using Domain;
using RepositorySQL;

namespace Services;

public class ClientService(IClientRepository _clientRepository) : IClientService
{
    public async Task<IList<Domain.Client>> GetClients(Domain.Client? filter)
    {
        var clientsData = await _clientRepository.GetAll(new RepositorySQL.Client()
        {
            Name = filter?.Name!,
            Email = filter?.Email!
        });

        var clients = clientsData.Select(client => new Domain.Client()
        {
            Number = new Guid(client.Number),
            Name = client.Name,
            Email = client.Email,
        }).ToList();

        return clients;
    }
    public async Task DeleteClient(Guid number)
    {
        await _clientRepository.DeleteClient(number);
    }
    public async Task<string> CheckIfEmailExists(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) 
            return string.Empty;

        var clients = await GetClients(new Domain.Client() { Email = email });
        if (clients.Any())
        {
            return "Email already in use";
        }

        return string.Empty;
    }
    public async Task RegisterNewClient(Domain.Client client)
    {

        client.RegisterNewClient();
        var clientData = new RepositorySQL.Client()
        {
            Number = client.Number.ToString(),
            Name = client.Name,
            Email = client.Email,
        };
        await _clientRepository.Create(clientData);
    }

    public async Task<Domain.Client> GetClientByNumber(Guid number)
    {
        var clientData = await _clientRepository.GetByNumber(number);
        var client = new Domain.Client()
        {
            Number = new Guid(clientData.Number),
            Name = clientData.Name,
            Email = clientData.Email,
        };
        return client;
    }
}
