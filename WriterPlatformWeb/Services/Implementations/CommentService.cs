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
}