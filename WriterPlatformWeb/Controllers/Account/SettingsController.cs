using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Account;

[Route("account/settings")]
[Authorize]
public class SettingsController : Controller
{
    private readonly IUserService _userService;

    public SettingsController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Settings()
    {
        return View();
    }

    [HttpPost("update-email")]
    public async Task<IActionResult> UpdateEmail([FromForm] string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            TempData["ErrorMessage"] = "Введите Email";
            return RedirectToAction(nameof(Settings));
        }

        var result = await _userService.UpdateEmailAsync(email);
        if (result)
        {
            TempData["SuccessMessage"] = "Email успешно обновлен.";
        }
        else
        {
            TempData["ErrorMessage"] = "Не удалось обновить email.";
        }

        return RedirectToAction(nameof(Settings));
    }

    [HttpPost("update-password")]
    public async Task<IActionResult> UpdatePassword([FromForm] string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            TempData["ErrorMessage"] = "Введите пароль";
            return RedirectToAction(nameof(Settings));
        }

        var result = await _userService.UpdatePasswordAsync(password);
        if (result)
        {
            TempData["SuccessMessage"] = "Пароль успешно обновлен.";
        }
        else
        {
            TempData["ErrorMessage"] = "Не удалось обновить пароль.";
        }

        return RedirectToAction(nameof(Settings));
    }
}