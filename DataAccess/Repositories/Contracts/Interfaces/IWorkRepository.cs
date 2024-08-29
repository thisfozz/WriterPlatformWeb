using DataAccess.Entities;

namespace DataAccess.Repositories.Contracts.Interfaces;

public interface IWorkRepository
{
    Task<IEnumerable<WorkEntity>> GetAllWorksAsync();
    Task<WorkEntity> PublishWorkAsync(string title, int genreId, int authorId, DateOnly publicationDate, string text);
    Task<WorkEntity?> GetWorkByIdAsync(int workId);
    Task<IEnumerable<WorkEntity>> SearchWorksAsync(string value);
    Task<IEnumerable<WorkEntity>> GetTopWorksByRatingAsync(int topCount = 50);
    Task<IEnumerable<WorkEntity>> GetTopWorksByCommentsAsync(int topCount = 50);
    Task<decimal?> GetCurrentRatingAsync(int workId);
    Task<bool> UpdateRatingAsync(int workId, decimal newAverageRating);
    Task<CommentEntity> AddCommentAsync(Guid userId, int workId, string text);
    Task<bool> DeleteCommentAsync(int commentId);
}