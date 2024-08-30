using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface ICommentService
{
    Task<CommentDTO> AddCommentAsync(int workId, Guid userId, string comment);
    Task<CommentDTO?> GetCommentByIdAsync(int commentId);
    Task<bool> DeleteCommentAsync(int commentId);
    Task<List<CommentDTO>> GetCommentsByWorkIdAsync(int workId);
}