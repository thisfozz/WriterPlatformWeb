using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface IRoleRepository
{
    Task<RoleEntity> GetRoleByIdAsync(int roleId);
    Task<IEnumerable<RoleEntity>> GetAllRolesAsync();
}