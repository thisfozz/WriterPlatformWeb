using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Models.ViewModel.Work;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Work;

[Route("works")]
public class WorkController : Controller
{
    private readonly IWorkService _workService;
    private readonly IGenreService _genreService;
    private readonly IAuthorService _authorService;
    private readonly ICommentService _commentService;
    private const int WordsPerPage = 200;

    public WorkController(IWorkService workService, IGenreService genreService, IAuthorService authorService, ICommentService commentService)
    {
        _workService = workService;
        _genreService = genreService;
        _authorService = authorService;
        _commentService = commentService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetWorkDetails([FromRoute] int id)
    {
        var work = await _workService.GetWorkByIdAsync(id);
        if (work == null)
        {
            return NotFound();
        }

        var comments = await _commentService.GetCommentsByWorkIdAsync(id);

        var viewModel = new WorkDetailsViewModel
        {
            Work = work,
            Comments = comments.ToArray()
        };

        return View("WorkDetails", viewModel);
    }

    [HttpGet("publish")]
    [Authorize]
    public async Task<IActionResult> PublishWorkForm()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        var genres = await _genreService.GetAllGenresAsync();

        var viewModel = new PublishWorkViewModel
        {
            Authors = authors,
            Genres = genres
        };

        return View("PublishWorkForm", viewModel);
    }

    [HttpPost("publish")]
    [Authorize]
    public async Task<IActionResult> PublishWork(PublishWorkViewModel model)
    {

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

    [HttpGet("read-text/{id}")]
    [Authorize]
    public async Task<IActionResult> ReadText([FromRoute] int id, int page = 1)
    {
        var fullText = await _workService.GetTextWork(id);

        if (string.IsNullOrEmpty(fullText))
        {
            return NotFound();
        }

        var words = fullText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


        var totalPages = (int)Math.Ceiling((double)words.Length / WordsPerPage);

        if (page < 1) page = 1;
        if (page > totalPages) page = totalPages;

        var pageWords = words.Skip((page - 1) * WordsPerPage).Take(WordsPerPage);

        var pageText = string.Join(" ", pageWords);

        var model = new PaginatedTextViewModel
        {
            WorkId = id,
            Text = pageText,
            CurrentPage = page,
            TotalPages = totalPages,
        };

        return View("Readtext", model);
    }
}