using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Models.ViewModel.User;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Account;

[Route("account/settings")]
[Authorize]
public class SettingsController : Controller
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public SettingsController(IUserService userService, IRoleService roleService)
    {
        _userService = userService;
        _roleService = roleService;
    }

    [HttpGet]
	public async Task<IActionResult> Settings()
	{
		var user = await _userService.GetUserIdAsync();
        var role = await _roleService.GetRoleByIdAsync(user.RoleId);


        if (user == null)
		{
			TempData["ErrorMessage"] = "Не удалось загрузить данные пользователя.";
			return RedirectToAction("Index", "Home");
		}

		var model = new UserViewModel
		{
			UserId = user.UserId,
			UserName = user.UserName,
			Email = user.Email,
            Password = user.Password,
			RoleName = role.RoleName
        };

		return View("Settings", model);
	}

    [HttpPost("update-settings")]
    public async Task<IActionResult> UpdateSettings(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var resultEmail = true;
            var resultPassword = true;
            var resultUsername = true;

            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                resultEmail = await _userService.UpdateEmailAsync(model.Email);
            }

            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                resultPassword = await _userService.UpdatePasswordAsync(model.Password);
            }

            if (!string.IsNullOrWhiteSpace(model.UserName))
            {
                resultUsername = await _userService.UpdateUsernameAsync(model.UserName);
            }

            if (resultEmail && resultPassword && resultUsername)
            {
                TempData["SuccessMessage"] = "Настройки успешно обновлены.";
            }
            else
            {
                TempData["ErrorMessage"] = "Не удалось обновить некоторые настройки.";
            }

            return RedirectToAction(nameof(Settings));
        }

        TempData["ErrorMessage"] = "Неверные данные.";
        return RedirectToAction(nameof(Settings));
    }
}