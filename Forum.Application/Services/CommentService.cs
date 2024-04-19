using Forum.Application.Repositories;
using Forum.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Application.Services
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(long id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }

        public async Task<Comment> AddCommentAsync(Comment comment)
        {
            return await _commentRepository.AddAsync(comment);
        }

        public async Task<Comment> UpdateCommentAsync(Comment comment)
        {
            return await _commentRepository.UpdateAsync(comment);
        }

        public async Task DeleteCommentAsync(long id)
        {
            await _commentRepository.DeleteAsync(id);
        }
    }
}