using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WriterPlatformWeb.Controllers.Admin;

[Route("admin")]
[Authorize(Roles = "Администратор,Administrator,Admin")]
public class AdminController : Controller
{
    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        return View("Admindashboard");
    }
}