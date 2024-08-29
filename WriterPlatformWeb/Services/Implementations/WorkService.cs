using AutoMapper;
using DataAccess.Repositories.Contracts.Interfaces;
using WriterPlatformWeb.Models.Work;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Services.Implementations;

public class WorkService : IWorkService
{
    private readonly IWorkRepository _workRepository;
    private readonly IMapper _mapper;

    public WorkService(IWorkRepository workRepository, IMapper mapper)
    {
        _workRepository = workRepository;
        _mapper = mapper;
    }

    public async Task<CommentDTO> AddCommentAsync(int userId, int workId, string text)
    {
        var comment = await _workRepository.AddCommentAsync(userId, workId, text);
        return _mapper.Map<CommentDTO>(comment);
    }

    public async Task<bool> DeleteCommentAsync(int commentId)
    {
        return await _workRepository.DeleteCommentAsync(commentId);
    }

    public async Task<decimal?> GetCurrentRatingAsync(int workId)
    {
        return await _workRepository.GetCurrentRatingAsync(workId);
    }

    public async Task<List<WorkDTO>> GetTopWorksByCommentsAsync(int topCount = 50)
    {
        var works = await _workRepository.GetTopWorksByCommentsAsync(topCount);
        return works.Select(w => _mapper.Map<WorkDTO>(w)).ToList();
    }

    public async Task<List<WorkDTO>> GetTopWorksByRatingAsync(int topCount = 50)
    {
        var works = await _workRepository.GetTopWorksByRatingAsync(topCount);
        return works.Select(w => _mapper.Map<WorkDTO>(w)).ToList();
    }

    public async Task<WorkDTO?> GetWorkByIdAsync(int workId)
    {
        var work = await _workRepository.GetWorkByIdAsync(workId);
        return _mapper.Map<WorkDTO>(work);
    }

    public async Task<WorkDTO> PublishWorkAsync(string title, int genreId, int authorId, DateOnly publicationDate, string text)
    {
        var work = await _workRepository.PublishWorkAsync(title, genreId, authorId, publicationDate, text);
        return _mapper.Map<WorkDTO>(work);
    }

    public async Task<List<WorkDTO>> SearchWorksAsync(string authorName, string title, int genreId)
    {
        var works = await _workRepository.SearchWorksAsync(authorName, title, genreId);
        return works.Select(w => _mapper.Map<WorkDTO>(w)).ToList();
    }

    public async Task<bool> UpdateRatingAsync(int workId, decimal newAverageRating)
    {
        return await _workRepository.UpdateRatingAsync(workId, newAverageRating);
    }
}