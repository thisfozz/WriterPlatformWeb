using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Controllers.Comment;

public class CommentController : Controller
{
    private readonly IUserService _userService;
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService, IUserService userService)
    {
        _commentService = commentService;
        _userService = userService;
    }


    [HttpPost("{workId}/add-comment")]
    [Authorize]
    public async Task<IActionResult> AddComment(int workId, [FromForm] string comment)
    {
        var userDto = await _userService.GetUserByIdAsync();

        if (userDto == null)
        {
            return Unauthorized();
        }

        if (string.IsNullOrWhiteSpace(comment))
        {
            TempData["ErrorMessage"] = "Комментарий не может быть пустым.";
            return RedirectToAction("GetWorkDetails", "Work", new { workId });
        }

        await _commentService.AddCommentAsync(workId, userDto.UserId, comment);

        TempData["SuccessMessage"] = "Комментарий успешно добавлен.";
        return RedirectToAction("GetWorkDetails", "Work", new { workId });
    }

    [HttpPost("{workId}/delete-comment/{commentId}")]
    [Authorize(Roles = "Администратор,Administrator,Admin")]
    public async Task<IActionResult> DeleteComment(int workId, int commentId)
    {
        var comment = await _commentService.GetCommentByIdAsync(commentId);

        if(comment == null)
        {
            return NotFound();
        }
        var result = await _commentService.DeleteCommentAsync(commentId);

        if(result)
        {
            TempData["SuccessMessage"] = "Комментарий успешно удален.";
        }
        else
        {
            TempData["ErrorMessage"] = "Не удалось удалить комментарий.";
        }
        return RedirectToAction("GetWorkDetails", "Work", new { workId });
    }
}