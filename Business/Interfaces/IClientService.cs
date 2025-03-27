using Data.Entities;
using Domain.Dtos;
using Domain.Models;

namespace Business.Interfaces;

public interface IClientService
{
    Task<bool> CreateClientAsync(ClientRegistrationDto registrationForm);
    Task<IEnumerable<Client>> GetAllClientsAsync();
    Task<Client> GetClientByIdAsync(int id);
    Task<bool> UpdateClientAsync(int id, ClientRegistrationDto updateForm);
    Task<bool> DeleteClientAsync(int id);
}