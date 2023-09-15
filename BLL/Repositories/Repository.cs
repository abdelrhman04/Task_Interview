using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BLL
{
    public  class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _context;
        private DbSet<TEntity> dbSet;
        ICacheService cacheService;
        //Constrouctor
        public Repository(DbContext context, ICacheService cacheService)
        {
            this._context = context;
            this.dbSet = context.Set<TEntity>();
            this.cacheService = cacheService;
        }
        //Method Read
        
       
        public async Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken, string key)
        {
            var elements = await cacheService.GetAsync(key, async () =>
            {
                return await _context.Set<TEntity>().ToListAsync(cancellationToken);
            });
            return elements;
        }
 
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, string key)
        {
            await cacheService.RemoveByPrefixAsync(key, cancellationToken);
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken, string key)
        {
            await cacheService.RemoveByPrefixAsync(key, cancellationToken);
            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
 
       
        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, string key)
        {
            await cacheService.RemoveByPrefixAsync(key, cancellationToken);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }

       
        public async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, string key, Expression<Func<TEntity, bool>>? value = null)
        {
            var elements = await cacheService.GetAsync(key, async () =>
            {
                return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(value, cancellationToken);
            });
            return elements;
        }
        
    }
}
