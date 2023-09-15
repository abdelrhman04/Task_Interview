using System.Linq.Expressions;

namespace BLL
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, string key, Expression<Func<TEntity, bool>>? value = null);
        Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken, string key);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, string key);
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken, string key);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, string key);
    }
}
