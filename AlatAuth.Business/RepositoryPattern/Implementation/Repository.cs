using AlatAuth.Common.RepositoryPattern.Interface;
using AlatAuth.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AlatAuth.Common.RepositoryPattern.Implementation
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).AnyAsync();
        }

        public async Task<List<T>> GetAllAsync(string includeProperties = "", Expression<Func<T, bool>> filter = null, bool isActive = false, int take = 0)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (take > 0)
            {
                query = query.Take(take);
            }
            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllAsyncByDesc<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, string includeProperties = "", Expression<Func<T, bool>> filter = null, bool isActive = false, int take = 0)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            query.OrderByDescending(orderBy);
            if (take > 0)
            {
                query = query.Take(take);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefaultAsync(string includeProperties = "", Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _context.Set<T>();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }
            return await query.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<T> GetFirstOrDefaultOrderByDescAsync<TOrderBy>(string includeProperties = "", Expression<Func<T, bool>> filter = null, Expression<Func<T, TOrderBy>> orderBy = null)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (string includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query.OrderByDescending(orderBy);
            }
            return await query.FirstOrDefaultAsync();
        }

    }

}
