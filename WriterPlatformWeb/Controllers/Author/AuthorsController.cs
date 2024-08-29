using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Author;

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
        if (authors == null)
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
}