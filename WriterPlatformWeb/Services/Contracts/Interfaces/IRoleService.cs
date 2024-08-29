using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface IRoleService
{
    Task<bool> CreateRoleAsync(string roleName);
    Task<RoleDTO?> GetRoleByIdAsync(int roleId);
    Task<List<RoleDTO>> GetAllRolesAsync();
}