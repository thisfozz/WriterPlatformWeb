using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Author;

[Route("author-details")]
public class AuthorDetailsController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorDetailsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpPost("update-author")]
    [Authorize(Roles = "Администратор,Administrator,Admin")]
    public async Task<IActionResult> UpdateAuthor([FromForm] int authorId, [FromForm] string firstName, [FromForm] string lastName)
    {
        if (authorId <= 0)
        {
            TempData["ErrorMessage"] = "Некорректный идентификатор Автора.";
            return RedirectToAction("Dashboard", "Admin");
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            TempData["ErrorMessage"] = "Введите Имя автора";
            return RedirectToAction("Dashboard", "Admin");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            TempData["ErrorMessage"] = "Введите Фамилию автора";
            return RedirectToAction("Dashboard", "Admin");
        }

        var result = await _authorService.UpdateAuthorAsync(authorId, firstName, lastName);

        if (result)
        {
            TempData["SuccessMessage"] = "Автор успешно обновлен.";
        }
        else
        {
            TempData["ErrorMessage"] = "Ошибка при обновлении автора";
        }

        return RedirectToAction("Dashboard", "Admin");
    }
}