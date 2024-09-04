using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Models.ViewModel;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers;

public class HomeController : Controller
{
    private readonly IWorkService _workService;
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, IWorkService workService, IMapper mapper)
    {
        _workService = workService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var works = await _workService.GetAlllWorksAsync();

        if (works == null && !works.Any())
        {
            return NotFound();
        }

        var workViewModels = _mapper.Map<List<WorkViewModel>>(works);

        return View("Works", workViewModels);
    }

    [HttpGet]
    public async Task<IActionResult> SearchWorks([FromQuery] string value)
    {
        var works = await _workService.SearchWorksAsync(value);
        return View("Index", works);
    }

    // Показать топовые произведения по рейтингу
    [HttpGet("top-by-rating")]
    public async Task<IActionResult> TopWorksByRating([FromQuery] int topCount = 50)
    {
        var works = await _workService.GetTopWorksByRatingAsync(topCount);

        if (works != null)
        {
            ViewBag.TopType = "С высоким рейтингом";
            return View("Works", works);
        }
        return NotFound();
    }

    // Показать топовые произведения по комментариям
    [HttpGet("top-by-comments")]
    public async Task<IActionResult> TopWorksByComments([FromQuery] int topCount = 50)
    {
        var works = await _workService.GetTopWorksByCommentsAsync(topCount);

        if (works != null)
        {
            ViewBag.TopType = "Популярные";
            return View("Works", works);
        }
        return NotFound();
    }
}