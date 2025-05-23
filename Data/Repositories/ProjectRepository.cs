﻿using System.Diagnostics;
using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Domain.Extensions;
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

    public override async Task<IEnumerable<Project>> GetAllModelsAsync(bool orderByDescending = false,
            Expression<Func<ProjectEntity, object>>? sortBy = null,
            Expression<Func<ProjectEntity, bool>>? where = null,
            params Expression<Func<ProjectEntity, object>>[] includes)
    {
        try
        {
            var entities = await _context.Projects
                .Include(x => x.Status)
                .Include(x => x.Client)
                .Include(x => x.ProjectMembers)
                    .ThenInclude(x => x.Member)
                .ToListAsync();

            var projects = entities.Select(entity => entity.MapTo<Project>());
            return projects;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving projects :: {ex.Message}");
            return null!;
        }
    }

    public override async Task<Project> GetModelAsync(
        Expression<Func<ProjectEntity, bool>> where,
        params Expression<Func<ProjectEntity, object>>[] includes)
    {
        ArgumentNullException.ThrowIfNull(where);

        try
        {
            IQueryable<ProjectEntity> query = _context.Projects
                .Include(x => x.Status)
                .Include(x => x.Client)
                .Include(x => x.ProjectMembers)
                    .ThenInclude(pm => pm.Member);

            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }

            var entity = await query.FirstOrDefaultAsync(where);

            ArgumentNullException.ThrowIfNull(entity, "Project not found");

            return entity.MapTo<Project>();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error retrieving Project entity :: {ex.Message}");
            return default!; 
        }
    }

}