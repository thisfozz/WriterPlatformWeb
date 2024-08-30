using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Models.ViewModel;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Work;

[Route("works")]
public class WorkController : Controller
{
    private readonly IWorkService _workService;
    private readonly IGenreService _genreService;
    private readonly IAuthorService _authorService;
    private readonly ICommentService _commentService;

    public WorkController(IWorkService workService, IGenreService genreService, IAuthorService authorService, ICommentService commentService)
    {
        _workService = workService;
        _genreService = genreService;
        _authorService = authorService;
        _commentService = commentService;
    }

    // Показать форму для создания нового произведения
    [HttpGet("publish")]
    public async Task<IActionResult> PublishWorkForm()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        var genres = await _genreService.GetAllGenresAsync();

        var viewModel = new PublishWorkViewModel
        {
            Authors = authors,
            Genres = genres
        };

        return View("PublishWorkForm", viewModel); // НЕ ЗАБЫТЬ СОЗДАТЬ ПРЕДСТАВЛЕНИЕ
    }


    // Обработать отправленную форму для создания нового произведения
    [HttpPost("publish")]
    public async Task<IActionResult> PublishWork(PublishWorkViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Authors = await _authorService.GetAllAuthorsAsync();
            model.Genres = await _genreService.GetAllGenresAsync();

            return View("PublishWorkForm", model);
        }

        var work = await _workService.PublishWorkAsync(model.Work.Title, model.SelectedGenreId, model.SelectedAuthorId, model.Work.PublicationDate, model.Work.Text);

        if(work != null)
        {
            TempData["SuccessMessage"] = "Произведение успешно опубликовано.";
            return RedirectToAction("GetWorkDetails", new { workId = work.WorkId });
        }

        TempData["ErrorMessage"] = "Ошибка при публикации произведения";

        // Это если пройзодет ошибка загрузки
        model.Authors = await _authorService.GetAllAuthorsAsync();
        model.Genres = await _genreService.GetAllGenresAsync();

        return View("PublishWorkForm", model);
    }

    // Показать список всех произведений
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

    // Показать детали конкретного произведения
    [HttpGet("{workId}")]
    public async Task<IActionResult> GetWorkDetails(int workId)
    {
        var work = await _workService.GetWorkByIdAsync(workId);
        if (work == null)
        {
            return NotFound();
        }

        var comments = await _commentService.GetCommentsByWorkIdAsync(workId);

        var viewModel = new WorkDetailsViewModel
        {
            Work = work,
            Comments = comments.ToArray()
        };

        return View("WorkDetails", viewModel); // НЕ ЗАБЫТЬ СОЗДАТЬ ВЬЮШКУ В КОТОРОЙ БУДЕТ ИСПОЛЬЗОВАН WorkDetailsViewModel
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