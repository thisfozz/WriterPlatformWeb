using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Implementations;

public class RoleRepository : IRoleRepository
{
    private readonly WriterPlatformContext _context;

    public RoleRepository(WriterPlatformContext context)
    {
        _context = context;
    }

    public async Task<RoleEntity?> GetRoleByIdAsync(int roleId)
    {
        return await _context.Roles.FindAsync(roleId);
    }

    public async Task<IEnumerable<RoleEntity>> GetAllRolesAsync()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<bool> CreateRoleAsync(string roleName)
    {
        var role = new RoleEntity
        {
            Name = roleName 
        };

        _context.Roles.Add(role);
        await _context.SaveChangesAsync();

        return true;
    }
}