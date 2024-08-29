using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WriterPlatformWeb.Models;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers;

public class HomeController : Controller
{
    private readonly IRoleService _roleService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IRoleService roleService)
    {
        _logger = logger;
        _roleService = roleService;
    }

    public async Task<IActionResult> Index()
    {
        await _roleService.CreateRoleAsync("test");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}