using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace studyRats.Service.Platform.Domain.Abstractions.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Retrieval methods are Async because they trigger I/O (database queries)
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        // State change methods remain Sync as they primarily interact with the Change Tracker
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}