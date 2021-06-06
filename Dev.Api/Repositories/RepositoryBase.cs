using Dev.Api.Data;
using Dev.Api.Entities;
using Dev.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Dev.Api.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>  where TEntity : class
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected RepositoryBase(DataContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity obj)
        {
            _dbSet.Add(obj);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity obj)
        {
            _dbSet.Update(obj);
            await SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity obj)
        {
            _dbSet.Remove(obj);
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async  Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }        

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
