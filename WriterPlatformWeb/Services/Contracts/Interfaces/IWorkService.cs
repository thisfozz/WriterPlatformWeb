using WriterPlatformWeb.Models.Work;

namespace WriterPlatformWeb.Services.Contracts.Interfaces;

public interface IWorkService
{
    Task<List<WorkDTO>> GetAlllWorksAsync();
    Task<WorkDTO> PublishWorkAsync(string title, int genreId, int authorId, DateOnly publicationDate, string text);
    Task<WorkDTO?> GetWorkByIdAsync(int workId);
    Task<List<WorkDTO>> SearchWorksAsync(string value);
    Task<List<WorkDTO>> GetTopWorksByRatingAsync(int topCount = 50);
    Task<List<WorkDTO>> GetTopWorksByCommentsAsync(int topCount = 50);
    Task<decimal?> GetCurrentRatingAsync(int workId);
    Task<bool> UpdateRatingAsync(int workId, decimal newAverageRating);
    Task<string> GetTextWork(int workId);
}