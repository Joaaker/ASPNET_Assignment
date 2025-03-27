using Data.Entities;
using Domain.Dtos;

namespace Business.Factories;

public class MemberAddressFactory
{
    public static MemberAddressEntity CreateEntity(MemberRegistrationFormDto form) => new()
    {
        StreetName = form.StreetName!,
        PostalCode = form.PostalCode!,
        City = form.City!
    };
}