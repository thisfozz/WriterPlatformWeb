using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Work;

[Route("works")]
public class WorkController : Controller
{
    private readonly IWorkService _workService;

    public WorkController(IWorkService workService)
    {
        _workService = workService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllWorks()
    {
        var works = await _workService.GetAlllWorksAsync();

        if(works == null && !works.Any())
        {
            return NotFound();
        }

        return View("Works", works); // НЕ ЗАБЫТЬ СОЗДАТЬ ПРЕДСТАВЛЕНИЕ
    }

    [HttpGet("{workId}")]
    public async Task<IActionResult> GetWorkDetails(int workId)
    {
        var work = await _workService.GetWorkByIdAsync(workId);
        if (work == null)
        {
            return NotFound();
        }

        return View("WorkDetails", work); // НЕ ЗАБЫТЬ СОЗДАТЬ ПРЕДСТАВЛЕНИЕ
    }

    [HttpGet("publish")]
    public IActionResult PublishWorkForm()
    {
        return View("PublishWorkForm"); // НЕ ЗАБЫТЬ СОЗДАТЬ ПРЕДСТАВЛЕНИЕ

    }

    [HttpPost("publish")]
    public async Task<IActionResult> PublishWork([FromForm] string title, [FromForm] int genreId, [FromForm] int authorId, [FromForm] DateOnly publicationDate, [FromForm] string text)
    {
        var work = await _workService.PublishWorkAsync(title, genreId, authorId, publicationDate, text);

        if (work != null)
        {
            TempData["SuccessMessage"] = "Произведение успешно опубликовано.";
            return RedirectToAction("GetWorkDetails", new { workId = work.WorkId });
        }

        ModelState.AddModelError("", "Ошибка при публикации произведения");
        return View("PublishWorkForm");
    }

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