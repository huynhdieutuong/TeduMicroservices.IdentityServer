using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace TeduMicroservices.IDP.Infrastructure.Domains.Interfaces;

public interface IRepositoryBase<T, K> where T : EntityBase<K>
{
    #region Query
    IQueryable<T> FindAll(bool trackChange = false);
    IQueryable<T> FindAll(bool trackChange = false, params Expression<Func<T, object>>[] includeProperties);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);
    IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
        params Expression<Func<T, object>>[] includeProperties);

    Task<T> GetByIdAsync(K id);
    Task<T> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);
    #endregion

    #region Action
    Task<K> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task UpdateListAsync(IEnumerable<T> entities);
    Task DeleteAsync(T entity);
    Task DeleteListAsync(IEnumerable<T> entities);
    #endregion

    Task<int> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task EndTransactionAsync();
    Task RollbackTransactionAsync();
}
