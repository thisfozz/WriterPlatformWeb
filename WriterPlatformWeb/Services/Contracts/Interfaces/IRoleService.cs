using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface IRoleService
{
    Task<RoleDTO?> GetRoleByIdAsync(int roleId);
    Task<List<RoleDTO>> GetAllRolesAsync();
}