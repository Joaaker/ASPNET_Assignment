using Data.Entities;
using Domain.Dtos;
using Domain.Models;

namespace Business.Factories;

public class ClientFactory
{
    public static ClientEntity CreateEntity(ClientRegistrationDto registrationForm) => new()
    {
        ClientName = registrationForm.ClientName,
        Email = registrationForm.Email,
        PhoneNumber = registrationForm.Phone
    };

    public static ClientEntity CreateEntity(ClientRegistrationDto registrationForm, int id) => new()
    {
        Id = id,
        ClientName = registrationForm.ClientName,
        Email = registrationForm.Email,
        PhoneNumber = registrationForm.Phone
    };

    public static Client CreateModel(ClientEntity entity) => new()
    {
        ClientName = entity.ClientName,
        Email = entity.Email,
        Phone = entity.PhoneNumber
    };
}