using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Author;

public class AuthorSearchController : Controller
{
    private readonly IAuthorService _authorService;

    public AuthorSearchController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet("search-author")]
    public async Task<IActionResult> SearchAuthors([FromQuery] string firstnameOrLastname)
    {

        if (string.IsNullOrWhiteSpace(firstnameOrLastname))
        {
            ModelState.AddModelError("", "Введите Имя или Фамилию автора");
            return RedirectToAction("ManageAuthors", "Authors");
        }

        var authors = await _authorService.SearchAuthorsAsync(firstnameOrLastname);
        return RedirectToAction("ManageAuthors", "Authors", new { authors });
    }
}