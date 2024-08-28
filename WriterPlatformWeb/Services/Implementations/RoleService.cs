using AutoMapper;
using DataAccess.Repositories.Contracts.Interfaces;
using WriterPlatformWeb.Models.Work;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Services.Implementations;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public RoleService(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<RoleDTO?> GetRoleByIdAsync(int roleId)
    {
        return _mapper.Map<RoleDTO>(await _roleRepository.GetRoleByIdAsync(roleId));
    }

    public async Task<List<RoleDTO>> GetAllRolesAsync()
    {
        var roles = await _roleRepository.GetAllRolesAsync();
        return roles.Select(r => _mapper.Map<RoleDTO>(r)).ToList();
    }
}