using System.Linq.Expressions;
using Data.Entities;
using Domain.Dtos;
using Domain.Models;

namespace Business.Interfaces;

public interface IClientService
{
    Task<IResponseResult> CreateClientAsync(ClientRegistrationDto registrationForm);
    Task<IResponseResult<IEnumerable<Client>>> GetAllClientsAsync();
    Task<IResponseResult> GetClientByExpressionAsync(Expression<Func<ClientEntity, bool>> expression);
    Task<IResponseResult> UpdateClientAsync(int id, ClientRegistrationDto updateForm);
    Task<IResponseResult> DeleteClientAsync(int id);
}