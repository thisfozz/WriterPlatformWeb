using AutoMapper;
using DataAccess.Repositories.Contracts.Interfaces;
using WriterPlatformWeb.Models.Work;
using WriterPlatformWeb.Services.Contracts.Interfaces;

namespace WriterPlatformWeb.Services.Implementations;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<List<CommentDTO>> GetCommentsByWorkIdAsync(int workId)
    {
        var comments = await _commentRepository.GetCommentsByWorkIdAsync(workId);
        return comments.Select(c => _mapper.Map<CommentDTO>(c)).ToList();
    }

    public async Task<CommentDTO> AddCommentAsync(int workId, Guid userId, string comment)
    {
        var newComment = await _commentRepository.AddCommentAsync(workId, userId, comment);
        return _mapper.Map<CommentDTO>(newComment);
    }

    public async Task<bool> DeleteCommentAsync(int commentId)
    {
        return await _commentRepository.DeleteCommentAsync(commentId);
    }

    public async Task<CommentDTO?> GetCommentByIdAsync(int commentId)
    {
        var comment = await _commentRepository.GetCommentByIdAsync(commentId);
        return comment == null ? null : _mapper.Map<CommentDTO>(comment);
    }
}