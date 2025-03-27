using Data.Entities;
using Domain.Dtos;
using Domain.Models;

namespace Business.Interfaces;

public interface IClientService
{
    Task<IResponseResult> CreateClientAsync(ClientRegistrationDto registrationForm);
    Task<IResponseResult> GetAllClientsAsync();
    Task<IResponseResult> GetClientByIdAsync(int id);
    Task<IResponseResult> UpdateClientAsync(int id, ClientRegistrationDto updateForm);
    Task<IResponseResult> DeleteClientAsync(int id);
}