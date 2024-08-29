using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WriterPlatformWeb.Models;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers;

public class HomeController : Controller
{
    private readonly IWorkService _workService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IWorkService workService)
    {
        _logger = logger;
        _workService = workService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> SearchWorks([FromQuery] string value)
    {
        var works = await _workService.SearchWorksAsync(value);
        return View("Index", works);
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