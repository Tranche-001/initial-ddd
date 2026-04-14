using System.Linq.Expressions;

namespace studyRats.Service.Platform.Domain.Abstractions;
public interface IRepository<TEntity> where TEntity : class
{
    TEntity GetById(int id);
    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entities);
}
