using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Author;

[Route("author")]
public class AuthorsController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet]
    public async Task<IActionResult> ManageAuthors()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        if (authors == null || !authors.Any())
        {
            return NoContent();
        }
        return View(authors);
    }

    [HttpPost("add-author")]
    [Authorize]
    public async Task<IActionResult> AddAuthor([FromForm] string firstName, [FromForm] string lastName)
    {
        // Добавлена дополнительная серверная проверка потому что может просто офнуть JavaScript и запрос будет отправлен с пустой строкой
        if (string.IsNullOrWhiteSpace(firstName))
        {
            TempData["ErrorMessage"] = "Введите Имя автора";
            return View(nameof(ManageAuthors));
        }

        // Разделение проверки firstName и lastName было сделано для интуитивности пользователя
        if (string.IsNullOrWhiteSpace(lastName))
        {
            TempData["ErrorMessage"] = "Введите Фамилию автора";
            return View(nameof(ManageAuthors));
        }

        var author = await _authorService.AddAuthorAsync(firstName, lastName);

        if (author != null)
        {
            TempData["SuccessMessage"] = "Автор успешно создан.";
            return RedirectToAction(nameof(ManageAuthors));
        }

        TempData["ErrorMessage"] = "Не удалось создать автора";
        return RedirectToAction(nameof(ManageAuthors));
    }

    [HttpPost("delete-author")]
    [Authorize(Roles = "Администратор,Administrator")]
    public async Task<IActionResult> DeleteAuthor([FromForm] int authorId)
    {
        if (authorId <= 0)
        {
            TempData["ErrorMessage"] = "Некорректный идентификатор Автора.";
            return RedirectToAction(nameof(ManageAuthors));
        }

        var result = await _authorService.DeleteAuthorAsync(authorId);

        if (result)
        {
            TempData["SuccessMessage"] = "Автор успешно удален.";
        }
        else
        {
            TempData["ErrorMessage"] = "Ошибка при удалении автора";
        }

        return RedirectToAction(nameof(ManageAuthors));
    }
}