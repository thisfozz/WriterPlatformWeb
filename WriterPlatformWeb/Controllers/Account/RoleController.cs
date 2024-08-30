using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Account;

[Route("roles")]
[Authorize(Roles = "Администратор,Administrator,Admin")]
public class RoleController : Controller
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService, IUserService userService)
    {
        _roleService = roleService;
        _userService = userService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateRole([FromForm] string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            TempData["ErrorMessage"] = "Имя роли не может быть пустым.";
            return RedirectToAction("Dashboard", "Admin");
        }

        var result = await _roleService.CreateRoleAsync(roleName);
        if (result)
        {
            TempData["SuccessMessage"] = $"Роль {roleName} успешно создана.";
        }
        else
        {
            TempData["ErrorMessage"] = "Не удалось создать роль.";
        }
        return RedirectToAction("Dashboard", "Admin");
    }

    [HttpPost("update")]
    public async Task<IActionResult> UpdateRole([FromForm] string login, [FromForm] int roleId)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            TempData["ErrorMessage"] = "Введите логин";
            return RedirectToAction("Dashboard", "Admin");
        }

        if (roleId <= 0)
        {
            TempData["ErrorMessage"] = "Некорректный идентификатор роли.";
            return RedirectToAction("Dashboard", "Admin");
        }

        var result = await _userService.UpdateUserRoleAsync(login, roleId);
        if (result)
        {
            TempData["SuccessMessage"] = $"Роль пользователя {login} успешно обновлена.";
        }
        else
        {
            TempData["ErrorMessage"] = "Не удалось обновить роль.";
        }

        return RedirectToAction("Dashboard", "Admin");
    }
}