using Consul;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers;

[Authorize]
[Route("authors")]
public class AuthorsController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [NonAction]
    public IActionResult Index()
    {
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> ManageAuthors()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        if(authors == null)
        {
            return NoContent();
        }
        return View(authors);
    }

    [HttpPost("add-author")]
    public async Task<IActionResult> AddAuthor([FromForm] string firstName, [FromForm] string lastName)
    {
        // Добавлена дополнительная серверная проверка потому что может просто офнуть JavaScript и запрос будет отправлен с пустой строкой

        if (string.IsNullOrWhiteSpace(firstName))
        {
            ModelState.AddModelError("", "Введите Имя автора");
            return View(nameof(ManageAuthors));
        }

        // Разделение проверки firstName и lastName было сделано для интуитивности пользователя

        if (string.IsNullOrWhiteSpace(lastName))
        {
            ModelState.AddModelError("", "Введите Фамилию автора");
            return View(nameof(ManageAuthors));
        }

        var author = await _authorService.AddAuthorAsync(firstName, lastName);

        if (author != null)
        {
            TempData["SuccessMessage"] = "Автор успешно создан.";
            return RedirectToAction("ManageAuthors");
        }

        ModelState.AddModelError("", "Не удалось создать автора");
        return RedirectToAction("ManageAuthors");
    }

    [HttpPost("delete-author")]
    public async Task<IActionResult> DeleteAuthor([FromForm] int authorId)
    {
        if (authorId <= 0)
        {
            ModelState.AddModelError("", "Некорректный идентификатор Автора.");
            return View(nameof(ManageAuthors));
        }

        var result = await _authorService.DeleteAuthorAsync(authorId);

        if (result)
        {
            TempData["SuccessMessage"] = "Автор успешно удален.";
        }

        ModelState.AddModelError("", "Ошибка при удалении автора");
        return RedirectToAction("ManageAuthors");
    }

    [HttpPost("update-author")]
    public async Task<IActionResult> UpdateAuthor([FromForm] int authorId, [FromForm] string firstName, [FromForm] string lastName)
    {
        if (authorId <= 0)
        {
            ModelState.AddModelError("", "Некорректный идентификатор Автора.");
            return View(nameof(ManageAuthors));
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            ModelState.AddModelError("", "Введите Имя автора");
            return View(nameof(ManageAuthors));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            ModelState.AddModelError("", "Введите Фамилию автора");
            return View(nameof(ManageAuthors));
        }

        var result = await _authorService.UpdateAuthorAsync(authorId, firstName, lastName);

        if (result)
        {
            TempData["SuccessMessage"] = "Автор успешно обновлен.";
            return RedirectToAction("ManageAuthors");
        }

        ModelState.AddModelError("", "Ошибка при обновлении автора");
        return RedirectToAction("ManageAuthors");
    }
    [HttpPost("details-author")]
    public async Task<IActionResult> DetailsAuthor([FromForm] int authorId)
    {
        if (authorId <= 0)
        {
            ModelState.AddModelError("", "Некорректный идентификатор роли.");
            return View(nameof(ManageAuthors));
        }

        var author = await _authorService.GetAuthorByIdAsync(authorId);

        if(author == null)
        {
            return NotFound();
        }

        return View(author);
    }

    [HttpGet("search-author")]
    public async Task<IActionResult> SearchAuthors([FromQuery] string firstnameOrLastname)
    {

        if (string.IsNullOrWhiteSpace(firstnameOrLastname))
        {
            ModelState.AddModelError("", "Введите Имя или Фамилию автора");
            return View(nameof(ManageAuthors));
        }

        var authors = await _authorService.SearchAuthorsAsync(firstnameOrLastname);
        return View("ManageAuthors", authors);
    }
}