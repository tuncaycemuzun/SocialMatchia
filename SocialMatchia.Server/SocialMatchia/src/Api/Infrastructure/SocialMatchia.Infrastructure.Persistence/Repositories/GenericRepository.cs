using Microsoft.EntityFrameworkCore;
using SocialMatchia.Application.Interfaces.Repositories;
using SocialMatchia.Domain.Models;
using SocialMatchia.Infrastructure.Persistence.Context;
using System.Linq;
using System.Linq.Expressions;

namespace SocialMatchia.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly SocialMatchiaDbContext _context;
        protected DbSet<TEntity> _entity => _context.Set<TEntity>();

        public GenericRepository(SocialMatchiaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(TEntity entity)
        {
            await _entity.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _entity.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await Task.Run(() => _entity.Update(entity));
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            var data = await _entity.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (data != null)
            {
                _entity.Remove(data);
                return _context.SaveChanges() > 0;
            }

            return false;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            var ids = entities.Select(x => x.Id);
            var dataList = _entity.Where(x => ids.Contains(x.Id));
            _entity.RemoveRange(entities);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entity.AsNoTracking().AnyAsync(predicate);
        }

        public async Task<List<TEntity>?> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _entity;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                query = query.Include(include);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if(noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity? found = await _entity.FindAsync(id);

            if (found == null) return null;

            if (noTracking)
            {
                _context.Entry(found).State = EntityState.Detached;
            }

            if (includes != null && includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    if (include != null)
                    {
                        _context.Entry(found).Reference(include!).Load();
                    }
                }
            }

            return found;
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _entity.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = ApplyIncludes(query, includes);

            if (noTracking)
                query = query.AsNoTracking();

            var data = await query.FirstOrDefaultAsync();

            return data;
        }

        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes is not null)
            {
                foreach (var item in includes)
                {
                    query = query.Include(item);
                }
            }
            return query;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _entity.CountAsync(predicate);
        }
    }
}
