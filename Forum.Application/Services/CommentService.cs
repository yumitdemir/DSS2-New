using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Forum.Application.Repositories;
using Forum.Domain.Models;
using Forum.Infrastructure;

public class CommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CommentDetailDto>> GetAllCommentsAsync()
    {
        var comments = await _commentRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CommentDetailDto>>(comments);
    }

    public async Task<CommentDetailDto?> GetCommentByIdAsync(long id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        return comment == null ? null : _mapper.Map<CommentDetailDto>(comment);
    }

    public async Task<CommentDetailDto> AddCommentAsync(CreateCommentDto createCommentDto)
    {
        var comment = _mapper.Map<Comment>(createCommentDto);
        var addedComment = await _commentRepository.AddAsync(comment);
        return _mapper.Map<CommentDetailDto>(addedComment);
    }

    public async Task<CommentDetailDto?> UpdateCommentAsync(long id, UpdateCommentDto updateCommentDto)
    {
        var existingComment = await _commentRepository.GetByIdAsync(id);
        if (existingComment == null)
        {
            return null;
        }
        _mapper.Map(updateCommentDto, existingComment);
        var updatedComment = await _commentRepository.UpdateAsync(existingComment);
        return _mapper.Map<CommentDetailDto>(updatedComment);
    }

    public async Task DeleteCommentAsync(long id)
    {
        await _commentRepository.DeleteAsync(id);
    }
}