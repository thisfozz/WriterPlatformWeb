using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface IWorkService
{
    Task<WorkDTO> PublishWorkAsync(string title, int genreId, int authorId, DateOnly publicationDate, string text);
    Task<WorkDTO?> GetWorkByIdAsync(int workId);
    Task<List<WorkDTO>> SearchWorksAsync(string authorName, string title, int genreId);
    Task<List<WorkDTO>> GetTopWorksByRatingAsync(int topCount = 50);
    Task<List<WorkDTO>> GetTopWorksByCommentsAsync(int topCount = 50);
    Task<decimal?> GetCurrentRatingAsync(int workId);
    Task<bool> UpdateRatingAsync(int workId, decimal newAverageRating);
    Task<CommentDTO> AddCommentAsync(Guid userId, int workId, string text);
    Task<bool> DeleteCommentAsync(int commentId);
}