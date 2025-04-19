using Data.Entities;
using Data.Contexts;
using Data.Interfaces;
using Domain.Models;

namespace Data.Repositories;

public class MemberAddressRepository(DataContext context) : BaseRepository<MemberAddressEntity, MemberAddress>(context), IMemberAddressRepository
{
}