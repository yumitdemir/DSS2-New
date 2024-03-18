using Forum.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Application.Repositories
{
    public interface IBaseRepository<TEntity>
        where TEntity : IDomainEntity
    {
        Task<TEntity?> GetByIdAsync(long? id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> SaveAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
