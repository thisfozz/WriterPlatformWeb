using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Models.ViewModel.Admin;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Admin;

[Route("admin")]
[Authorize(Roles = "Администратор,Administrator,Admin")]
public class AdminController : Controller
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IAuthorService _authorService;

    public AdminController(IUserService userService, IRoleService roleService, IAuthorService authorService)
    {
        _userService = userService;
        _roleService = roleService;
        _authorService = authorService;
    }

    [HttpGet("dashboard")]
    public async Task<IActionResult> Dashboard()
    {
        var users = await _userService.GetAllUsersAsync();
        var roles = await _roleService.GetAllRolesAsync();
        var authors = await _authorService.GetAllAuthorsAsync();


        var viewModel = new AdminViewModel
        {
            Users = users,
            Roles = roles,
            Authors = authors
        };

        return View("Admindashboard", viewModel);
    }
}