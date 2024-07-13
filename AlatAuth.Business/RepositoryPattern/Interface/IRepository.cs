using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AlatAuth.Common.RepositoryPattern.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllAsync(string includeProperties = "", Expression<Func<T, bool>> filter = null, bool isActive = false, int take = 0);
        Task<List<T>> GetAllAsyncByDesc<TOrderBy>(Expression<Func<T, TOrderBy>> orderBy, string includeProperties = "", Expression<Func<T, bool>> filter = null, bool isActive = false, int take = 0);
        Task<T> GetFirstOrDefaultAsync(string includeProperties = "", Expression<Func<T, bool>> filter = null);
        Task<T> GetFirstOrDefaultOrderByDescAsync<TOrderBy>(string includeProperties = "", Expression<Func<T, bool>> filter = null, Expression<Func<T, TOrderBy>> orderBy = null);
    }

}
