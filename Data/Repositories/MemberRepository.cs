using Data.Entities;
using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using Domain.Models;

namespace Data.Repositories;

public class MemberRepository(DataContext context) : BaseRepository<MemberEntity, Member>(context), IMemberRepository
{
    public override async Task<MemberEntity> GetAsync(Expression<Func<MemberEntity, bool>> expression)
    {
        if (expression == null)
            return null!;

        try
        {
            var entity = await _context.Members
                .Include(x => x.Address)
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