using BLL.Shared;
using CORE.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IRepository<TEntity>
    {
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken,string key);
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity> spec, 
            CancellationToken cancellationToken, string key);
        Task<TEntity> GetByIdAsync(ISpecification<TEntity> spec,
            CancellationToken cancellationToken, string key);
        Task<TEntity> GetEntityWithSpec(ISpecification<TEntity> spec,
            CancellationToken cancellationToken, string key);
        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, string key,
            Expression<Func<TEntity, bool>>? value = null);
        Task<IQueryable<TEntity>> Query(string key,Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);
        public Task<TEntity> GetByIdAsync_AsNotracking(CancellationToken cancellationToken, string key, Expression<Func<TEntity, bool>>? value = null);
        Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken, string key);
        Task<bool> AnyAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? filter = null);
        //-------------------------------










        Task<Nullable<int>> DeleteAsyncreturn(TEntity entity, CancellationToken cancellationToken,string key);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, string key);
       
        Task<ICollection<TEntity>> AddAsync(ICollection<TEntity> entity, CancellationToken cancellationToken, string key);
        Task UpdateAsync(ICollection<TEntity> entity, CancellationToken cancellationToken, string key);
       
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, string key);
        Task<TEntity> UpdateAsync_Return(TEntity entity, CancellationToken cancellationToken, string key);
        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, string key);
        //check if element found update or not found create
        Task CheckCreateORUpdate<T>(string key, T entity, Expression<Func<TEntity, bool>>? filter = null) where T : BaseClass;
        Task<TEntity> CheckCreateORUpdatereturn<T>(string key, T entity, Expression<Func<TEntity, bool>>? filter = null) where T : BaseClass;









      //--------------------
       IEnumerable<TEntity> ExecStoreProcedure(string sql, params object[] parameters);
        IEnumerable<TEntity> ExecQuery(string query);
    }
}
