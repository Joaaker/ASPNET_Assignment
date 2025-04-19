using Data.Entities;
using Domain.Models;

namespace Data.Interfaces;

public interface IMemberAddressRepository : IBaseRepository<MemberAddressEntity, MemberAddress>
{
}