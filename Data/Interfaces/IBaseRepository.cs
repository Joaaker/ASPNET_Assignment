using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> SaveAsync();
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity);
}