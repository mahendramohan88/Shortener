using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllAsQueryable();
        IQueryable<T> GetAllAsQueryable(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        Task<T> Get(int id);
        Task<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        Task<bool> Exists(int id);
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(int id);
    }
}
