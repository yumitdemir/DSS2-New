using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.Domain.Models;

namespace Forum.Application.Repositories
{
    public interface ITopicRepository
    {
        Task<IEnumerable<Topic>> GetAllAsync();
        Task<Topic?> GetByIdAsync(long id);
        Task<Topic> AddAsync(Topic topic);
        Task<Topic> UpdateAsync(Topic topic);
        Task DeleteAsync(long id);
    }
}