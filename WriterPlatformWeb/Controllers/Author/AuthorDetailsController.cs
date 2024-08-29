using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Author;

public class AuthorDetailsController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorDetailsController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpPost("update-author")]
    public async Task<IActionResult> UpdateAuthor([FromForm] int authorId, [FromForm] string firstName, [FromForm] string lastName)
    {
        if (authorId <= 0)
        {
            ModelState.AddModelError("", "Некорректный идентификатор Автора.");
            return RedirectToAction("ManageAuthors", "Authors");
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            ModelState.AddModelError("", "Введите Имя автора");
            return RedirectToAction("ManageAuthors", "Authors");
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            ModelState.AddModelError("", "Введите Фамилию автора");
            return RedirectToAction("ManageAuthors", "Authors");
        }

        var result = await _authorService.UpdateAuthorAsync(authorId, firstName, lastName);

        if (result)
        {
            TempData["SuccessMessage"] = "Автор успешно обновлен.";
            return RedirectToAction("ManageAuthors", "Authors");
        }

        ModelState.AddModelError("", "Ошибка при обновлении автора");
        return RedirectToAction("ManageAuthors", "Authors");
    }

    [HttpPost("details-author")]
    public async Task<IActionResult> DetailsAuthor([FromForm] int authorId)
    {
        if (authorId <= 0)
        {
            ModelState.AddModelError("", "Некорректный идентификатор роли.");
            return RedirectToAction("ManageAuthors", "Authors");
        }

        var author = await _authorService.GetAuthorByIdAsync(authorId);

        if (author == null)
        {
            return NotFound();
        }

        return View(author); // НЕ ЗАБЫТЬ СОЗДАТЬ ПРЕДСТАВЛЕНИЕ ДЛЯ ВОЗВРАТА ДЕТАЛЕЙ АВТОРА
    }
}