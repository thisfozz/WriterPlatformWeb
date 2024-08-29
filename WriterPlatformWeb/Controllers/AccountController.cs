using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Models.Auth;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginModel model, string returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            if(await _userService.AuthenticateUserAsync(model))
            {
                if(!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Пользователь не существует");
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            if (await _userService.RegisterUserAsync(model))
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _userService.Logout();
        return RedirectToAction("Login", "Account");
    }

    public IActionResult AccessDenied()
    {
        ViewData["errormessage"] = "Доступ к странице запрещен";
        return View();
    }
}