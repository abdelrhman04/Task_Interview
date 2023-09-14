using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BLL.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using CORE.Content;

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
        
        public virtual async Task<IQueryable<TEntity>> Query(string key,Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            var elements = await cacheService.GetAsync(key, async () =>
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                    query = query.Where(filter);

                if (orderBy != null)
                    query = orderBy(query);
               
                return query.AsNoTracking();
            });
            return elements;
        }
        //Method Read      
        public virtual async Task<TEntity>? GetByIdAsync(Guid id, CancellationToken cancellationToken, string key)
        {
            var elements = await cacheService.GetAsync<TEntity>(key, async () =>
            {
                return await _context.Set<TEntity>().FindAsync(id, cancellationToken);
            });

            return elements;
        }
        //Method to executed StoreProcedure
        public virtual IEnumerable<TEntity> ExecStoreProcedure(string sql, params object[] parameters)
        {
            var result = _context.Set<TEntity>().FromSqlRaw(sql, parameters);
            // _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            return result;
        }
        //Method to executed Query
        public virtual IEnumerable<TEntity> ExecQuery(string query)
        {
            var result = _context.Set<TEntity>().FromSqlRaw(query);
            return result;
        }
        //Method to Read
        public async Task<IReadOnlyList<TEntity>> ListAllAsync(CancellationToken cancellationToken, string key)
        {
            var elements = await cacheService.GetAsync(key, async () =>
            {
                return await _context.Set<TEntity>().ToListAsync(cancellationToken);
            });
            return elements;
        }
        //Method to  Write
        //Delete All key chaching 
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken, string key)
        {
            await cacheService.RemoveByPrefixAsync(key, cancellationToken);
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }
        //Method to Write
        //Delete All key chaching 
        public async Task<ICollection<TEntity>> AddAsync(ICollection<TEntity> entity, CancellationToken cancellationToken, string key)
        {
           await cacheService.RemoveByPrefixAsync(key, cancellationToken);
            foreach (var item  in entity)
            {
                await _context.Set<TEntity>().AddAsync(item, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            

            return entity;
        }
        //Method to Write
        //Delete All key chaching 
        public async Task UpdateAsync(ICollection<TEntity> entity, CancellationToken cancellationToken, string key)
        {
            await cacheService.RemoveByPrefixAsync(key, cancellationToken);
            foreach (var item in entity)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync(cancellationToken);

        }
        //Method to Write
        //Delete All key chaching 
        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, string key)
        {
            await cacheService.RemoveByPrefixAsync(key, cancellationToken);
            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
            
        }
        //Method to Write
        //Delete All key chaching 
        public async Task<TEntity> UpdateAsync_Return(TEntity entity, CancellationToken cancellationToken, string key)
        {
            await cacheService.RemoveByPrefixAsync(key, cancellationToken);
            _context.Attach(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
           

        }
        //Method to Write
        //Delete All key chaching 
        public async Task<Nullable<int>> DeleteAsyncreturn(TEntity entity, CancellationToken cancellationToken, string key)
        {
            await cacheService.RemoveByPrefixAsync(key, cancellationToken);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return default(int?);
        }
        //Method to Write
        //Delete All key chaching 
        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, string key)
        {
            await cacheService.RemoveByPrefixAsync(key, cancellationToken);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
        }
        //Specification_1
        public async Task<IReadOnlyList<TEntity>>? GetAllAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken, string key)
        {
            var elements =
                await cacheService.GetAsync(key, async () =>
            {
                return await ApplySpecification(spec).ToListAsync(cancellationToken);
            }, cancellationToken);
            return elements;
        }
        //Specification_2
        public async Task<TEntity> GetByIdAsync(ISpecification<TEntity> spec, CancellationToken cancellationToken, string key)
        {
            var elements = await cacheService.GetAsync(key, async () =>
            {
                return await ApplySpecification(spec).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
            });
            return elements;
            
        }
        public async Task<bool> AnyAsync(CancellationToken cancellationToken, Expression<Func<TEntity, bool>>? filter = null)
        {
            IQueryable<TEntity> query = dbSet;
            bool check = false;
            if (filter != null)
                check = query.Any(filter);
            return check;
        }
        //Specification_3
        public async Task<TEntity>? GetEntityWithSpec(ISpecification<TEntity> spec, CancellationToken cancellationToken, string key)
        {
            var elements = await cacheService.GetAsync(key, async () =>
            {
                return await ApplySpecification(spec)?.FirstOrDefaultAsync(cancellationToken);
            });
            return elements;
            

        }
        //Helper Method
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);

        }
        //Method to Read
        public async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, string key, Expression<Func<TEntity, bool>>? value = null)
        {
            var elements = await cacheService.GetAsync(key, async () =>
            {
                return await _context.Set<TEntity>().FirstOrDefaultAsync(value, cancellationToken);
            });
            return elements;
        }
        //Method to Read
        public async Task<TEntity> GetByIdAsync_AsNotracking(CancellationToken cancellationToken, string key, Expression<Func<TEntity, bool>>? value = null)
        {
            var elements = await cacheService.GetAsync(key, async () =>
            {
                return await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(value, cancellationToken);
            });
            return elements;  
        }
        //Method to Write 
        //Delete All key chaching 
        public async Task CheckCreateORUpdate<T>(string key, T entity, Expression<Func<TEntity, bool>>? filter = null)where T : BaseClass
        {
            await cacheService.RemoveByPrefixAsync(key,new CancellationToken());
            T query = await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter,new CancellationToken()) as T;
            
            if (query == null)
            {
               await AddAsync(entity as TEntity, new CancellationToken(), key);
            }
            else
            {
              entity.Id = query.Id;
              await  UpdateAsync(entity as TEntity, new CancellationToken(), key);
            }
            
        }
        public async Task<TEntity> CheckCreateORUpdatereturn<T>(string key, T entity, Expression<Func<TEntity, bool>>? filter = null) where T : BaseClass
        {
            await cacheService.RemoveByPrefixAsync(key, new CancellationToken());
            T query = await _context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(filter, new CancellationToken()) as T;

            if (query == null)
            {
              return   await AddAsync(entity as TEntity, new CancellationToken(), key);
            }
            else
            {
                entity.Id = query.Id;
                return await UpdateAsync_Return(entity as TEntity, new CancellationToken(), key);
            }

        }

    }
}
