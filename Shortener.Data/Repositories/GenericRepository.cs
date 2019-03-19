using Microsoft.EntityFrameworkCore;
using Shortener.Data.Interfaces;
using Shortener.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortener.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected DbSet<T> entities;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            entities = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await entities.Where(predicate).ToListAsync();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return entities.AsQueryable();
        }
        public IQueryable<T> GetAllAsQueryable(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return entities.Where(predicate).AsQueryable();
        }

        public async Task<T> Get(int id)
        {
            return await entities.FindAsync(id);
        }
        public async Task<T> Get(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return await entities.SingleOrDefaultAsync(predicate);
        }

        public async Task<bool> Exists(int id)
        {
            if (await entities.FindAsync(id) == null)
            {
                return false;
            }
            return true;
        }

        public async Task<T> Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await entities.FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
