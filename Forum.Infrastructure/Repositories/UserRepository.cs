using Forum.Application.Repositories;
using Forum.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Repositories
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DatabaseContext context) 
            : base(context)
        {
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            return Query
                .Where(e => e.Username!.ToUpper() == username.ToUpper())
                .FirstOrDefaultAsync();
        }

        public Task<bool> IsExistingUsernameAsync(string username)
        {
            return Query
                .Where(e => e.Username!.ToUpper() == username.ToUpper())
                .Select(e => e.Id)
                .AnyAsync();
        }

        public Task<bool> IsExistingUsernameAsync(long id, string username)
        {
            return Query
                .Where(e => e.Username!.ToUpper() == username.ToUpper())
                .Where(e => e.Id != id)
                .Select(e => e.Id)
                .AnyAsync();
        }

        public Task<bool> IsExistingEmailAsync(string email)
        {
            return Query
                .Where(e => e.Email!.ToUpper() == email.ToUpper())
                .Select(e => e.Id)
                .AnyAsync();
        }

        public Task<bool> IsExistingEmailAsync(long id, string email)
        {
            return Query
                .Where(e => e.Email!.ToUpper() == email.ToUpper())
                .Where(e => e.Id != id)
                .Select(e => e.Id)
                .AnyAsync();
        }
    }
}