using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class ProjectMemberRepository(DataContext context) : BaseRepository<ProjectMemberJunctionEntity>(context), IProjectMemberRepository
{
}