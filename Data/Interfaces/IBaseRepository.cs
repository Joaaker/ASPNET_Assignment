using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IBaseRepository<TEntity, TModel> where TEntity : class
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> AddAsync(TEntity entity);
    Task<bool> SaveAsync();
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);
    Task<TModel> GetAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TModel>> GetAllAsync(bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TSelect>> GetAllAsync<TSelect>(Expression<Func<TEntity, TSelect>> selector, bool orderByDescending = false, Expression<Func<TEntity, object>>? sortBy = null, Expression<Func<TEntity, bool>>? where = null, params Expression<Func<TEntity, object>>[] includes);
    Task<bool> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity);
}