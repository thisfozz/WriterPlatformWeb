using DataAccess.Contexts;
using DataAccess.Entities;
using DataAccess.Repositories.Contracts.Interfaces;

namespace DataAccess.Repositories.Implementations;

public class RoleRepository : IRoleRepository
{
    private readonly WriterPlatformContext _context;

    public RoleRepository(WriterPlatformContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RoleEntity>> GetAllRolesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<RoleEntity> GetRoleByIdAsync(int roleId)
    {
        throw new NotImplementedException();
    }
}