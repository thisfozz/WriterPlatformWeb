using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Work;

[Route("genres")]
public class GenreController : Controller
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    [HttpPost("create-genre")]
    [Authorize]
    public async Task<IActionResult> CreateGenre([FromForm] string genreName)
    {
        if(string.IsNullOrWhiteSpace(genreName))
        {
            TempData["ErrorMessage"] = "Имя жанра не может быть пустым.";
            return RedirectToAction("Dashboard", "Admin");
        }

        var result = await _genreService.CreateGenreAsync(genreName);

        if (result)
        {
            TempData["SuccessMessage"] = $"Жанр {genreName} успешно создан.";
        }
        else
        {
            TempData["ErrorMessage"] = "Не удалось создать жанр.";
        }
        return RedirectToAction("Dashboard", "Admin");
    }

    [HttpGet("all-genres")]
    public async Task<IActionResult> LoadGenres()
    {
        var genres = await _genreService.GetAllGenresAsync();

        if (genres == null || !genres.Any())
        {
            TempData["ErrorMessage"] = "Жанры не найдены.";
            return RedirectToAction("Dashboard", "Admin");
        }

        return View(genres); // НЕ ЗАБЫТЬ СОЗДАТЬ ПРЕДСТАВЛЕНИЕ
    }
}