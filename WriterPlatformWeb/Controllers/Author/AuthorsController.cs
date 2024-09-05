using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Author;

[Route("author")]
[Authorize(Roles = "Администратор,Administrator")]
public class AuthorsController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpPost("add-author")]
    public async Task<IActionResult> AddAuthor([FromForm] string firstName, [FromForm] string lastName)
    {
        // Добавлена дополнительная серверная проверка потому что может просто офнуть JavaScript и запрос будет отправлен с пустой строкой
        if (string.IsNullOrWhiteSpace(firstName))
        {
            TempData["ErrorMessage"] = "Введите Имя автора";
            return RedirectToAction("Dashboard", "Admin");
        }

        // Разделение проверки firstName и lastName было сделано для интуитивности пользователя
        if (string.IsNullOrWhiteSpace(lastName))
        {
            TempData["ErrorMessage"] = "Введите Фамилию автора";
            return RedirectToAction("Dashboard", "Admin");
        }

        var author = await _authorService.AddAuthorAsync(firstName, lastName);

        if (author != null)
        {
            TempData["SuccessMessage"] = "Автор успешно создан.";
            return RedirectToAction("Dashboard", "Admin");
        }

        TempData["ErrorMessage"] = "Не удалось создать автора";
        return RedirectToAction("Dashboard", "Admin");
    }

    [HttpPost("delete-author")]
    public async Task<IActionResult> DeleteAuthor([FromForm] int authorId)
    {
        if (authorId <= 0)
        {
            TempData["ErrorMessage"] = "Некорректный идентификатор Автора.";
            return RedirectToAction("Dashboard", "Admin");
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

        return RedirectToAction("Dashboard", "Admin");
    }
}