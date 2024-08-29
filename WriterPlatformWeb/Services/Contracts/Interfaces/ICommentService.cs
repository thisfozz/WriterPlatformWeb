using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface ICommentService
{
    Task<List<CommentDTO>> GetCommentsByWorkIdAsync(int workId);
}