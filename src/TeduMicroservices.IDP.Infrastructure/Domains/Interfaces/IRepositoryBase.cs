﻿using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq.Expressions;
using TeduMicroservices.IDP.Infrastructure.Common;

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

    #region Dapper
    Task<IReadOnlyList<TModel>> QueryAsync<TModel>(string sql,
                                                   object? param,
                                                   CommandType? commandType = CommandType.StoredProcedure,
                                                   IDbTransaction? transaction = null,
                                                   int? commandTimeout = SystemConstants.CommandTimeout) where TModel : EntityBase<K>;

    Task<TModel> QueryFirstOrDefaultAsync<TModel>(string sql,
                                                  object? param,
                                                  CommandType? commandType = CommandType.StoredProcedure,
                                                  IDbTransaction? transaction = null,
                                                  int? commandTimeout = SystemConstants.CommandTimeout) where TModel : EntityBase<K>;

    Task<TModel> QuerySingleAsync<TModel>(string sql,
                                          object? param,
                                          CommandType? commandType = CommandType.StoredProcedure,
                                          IDbTransaction? transaction = null,
                                          int? commandTimeout = SystemConstants.CommandTimeout) where TModel : EntityBase<K>;

    Task<int> ExecuteAsync(string sql,
                                   object? param,
                                   CommandType? commandType = CommandType.StoredProcedure,
                                   IDbTransaction? transaction = null,
                                   int? commandTimeout = SystemConstants.CommandTimeout);
    #endregion

    Task<int> SaveChangesAsync();
    Task<IDbContextTransaction> BeginTransactionAsync();
    Task EndTransactionAsync();
    Task RollbackTransactionAsync();
}
