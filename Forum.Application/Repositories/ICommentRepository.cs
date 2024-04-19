using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Domain.Models;

namespace Forum.Application.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(long id);
        Task<Comment> AddAsync(Comment comment);
        Task<Comment> UpdateAsync(Comment comment);
        Task DeleteAsync(long id);
    }
}