using Microsoft.EntityFrameworkCore;
using studyRats.Service.Platform.Domain.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace studyRats.Service.Platform.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DataContext _dbContext;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DataContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<TEntity>();
        }


        protected async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
        {
            try { return await action(); }
            catch (DbUpdateException ex) { /* Translate & Log here once */ throw; }
        }


        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            // FindAsync is the async version of Find
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            // ToListAsync triggers the database query asynchronously
            return await _dbSet.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }
        public void Add(TEntity entity)
        {
            // Even though there is a AddAsync, it serves a specific purpose that is not necessary for now. 
            _dbSet.Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            // EF Core does not have a RemoveAsync because Remove only 
            // changes the state of the entity to 'Deleted' in the tracker.
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}