using System.Diagnostics;
using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity, Project>(context), IProjectRepository
{
    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        try
        {
            var entities = await _context.Projects
                .Include(x => x.Status)
                .Include(x => x.Client)
                .Include(x => x.ProjectMembers)
                .ThenInclude(x => x.Member)
                .ToListAsync();

            return entities;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving entities :: {ex.Message}");
            return null!;
        }
    }

    public override async Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _context.Projects
                .Include(x => x.Status)
                .Include(x => x.Client)
                .Include(x => x.ProjectMembers)
                .ThenInclude(x => x.Member)
                .FirstOrDefaultAsync(expression);

            if (entity == null)
                return null!;

            return entity;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving project :: {ex.Message}");
            return null!;
        }
    }
}