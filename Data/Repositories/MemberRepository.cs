using Data.Entities;
using Data.Contexts;
using Data.Interfaces;

namespace Data.Repositories;

public class MemberRepository(DataContext context) : BaseRepository<MemberEntity>(context), IMemberRepository
{
}