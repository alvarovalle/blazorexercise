using Domain;
using Repository;

namespace Services;

public class ClientService(IClientRepository _clientRepository) : IClientService
{
    public async Task<IList<Domain.Client>> GetClients(Domain.Client? filter)
    {
        var clientsData = await _clientRepository.GetAll(new Repository.Client()
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
        await _clientRepository.Delete(number);
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
    private Repository.Client GetClientData(Domain.Client client)
    {
        var clientData = new Repository.Client()
        {
            Number = client.Number.ToString(),
            Name = client.Name,
            Email = client.Email,
        };
        return clientData;
    }
    public async Task RegisterClient(Domain.Client client)
    {
        Repository.Client? clientData = null;
        if (Guid.Empty.Equals(client.Number))
        {
            client.RegisterNewClient();
            clientData = GetClientData(client);
            await _clientRepository.Create(clientData);
        }
        else
        {
            clientData = GetClientData(client);
            await _clientRepository.Update(clientData);
        }
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
