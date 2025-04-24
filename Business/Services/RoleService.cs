using System.Diagnostics;
using Business.Interfaces;
using Business.Models;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Extensions;

namespace Business.Services;

public class RoleService(RoleManager<IdentityRole> roleManager) : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<IResponseResult<IEnumerable<Role>>> GetAllRolessAsync()
    {
        try
        {
            var rolesEntities = await _roleManager.Roles.ToListAsync();

            var roles = rolesEntities.Select(entity => entity.MapTo<Role>()).ToList();

            return ResponseResult<IEnumerable<Role>>.Ok(roles);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return ResponseResult<IEnumerable<Role>>.Error("Error retrieving members");
        }
    }
}