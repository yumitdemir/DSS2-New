using Forum.Domain.Models;
using System.Threading.Tasks;

namespace Forum.Application.Repositories
{
    public interface IUserRepository : IBaseRepository<User> 
    { 
        Task<User?> GetByUsernameAsync(string username);

        Task<bool> IsExistingUsernameAsync(string username);

        Task<bool> IsExistingUsernameAsync(long id, string username);

        Task<bool> IsExistingEmailAsync(string email);

        Task<bool> IsExistingEmailAsync(long id, string email);
    }
}
