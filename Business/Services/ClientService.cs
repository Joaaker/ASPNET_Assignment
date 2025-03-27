using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;
using Domain.Dtos;
using Domain.Models;

namespace Business.Services;

public class ClientService(IClientRepository clientRepository) : IClientService
{
    private readonly IClientRepository _clientRepository = clientRepository;

    public Task<bool> CreateClientAsync(ClientRegistrationDto registrationForm)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteClientAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ClientEntity>> GetAllClientsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Client> GetClientByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateClientAsync(int id, ClientRegistrationDto updateForm)
    {
        throw new NotImplementedException();
    }
}