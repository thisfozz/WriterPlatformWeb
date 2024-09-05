using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Models.Auth;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Account;

[Route("account")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("login")]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["Title"] = "Авторизация";
        ViewData["ReturnUrl"] = returnUrl; // Временно не используется

        return View("Login");
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] LoginModel model, string returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            if (await _userService.AuthenticateUserAsync(model))
            {
                return string.IsNullOrEmpty(returnUrl) ? RedirectToAction("Index", "Home") : Redirect(returnUrl);
            }

            TempData["ErrorMessage"] = "Пользователь не существует";
        }

        return View(model);
    }

    [HttpGet("register")]
    [AllowAnonymous]
    public IActionResult Register(string returnUrl = null)
    {
        ViewData["Title"] = "Регистрация";
        ViewData["ReturnUrl"] = returnUrl; // Временно не используется

        return View("Register");
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromForm] RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _userService.RegisterUserAsync(model))
            {
                return RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = "Некорректные логин и(или) пароль";
        }

        return View(model);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _userService.LogoutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpPost("delete")]
    [Authorize]
    public async Task<IActionResult> DeleteAccount()
    {
        var result = await _userService.DeleteUserAsync();
        if (result)
        {
            TempData["SuccessMessage"] = "Аккаунт успешно удален";
            return RedirectToAction("Index", "Home");
        }
        TempData["ErrorMessage"] = "Что-то пошло не так";
        return View("Settings");
    }
}