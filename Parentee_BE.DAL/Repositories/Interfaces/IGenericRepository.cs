using System.Linq.Expressions;
using Parentee_BE.DAL.Data.Metadatas;
using Microsoft.EntityFrameworkCore.Query;

namespace Parentee_BE.DAL.Data.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    #region Get Async
    Task<TEntity> FirstOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    
    Task<TResult> FirstOrDefaultAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    
    Task<ICollection<TEntity>> GetListAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int? take = null,
        int? skip = null);
    
    Task<ICollection<TResult>> GetListAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int? take = null,
        int? skip = null);
    
    Task<PagingResponse<TEntity>> GetPagingListAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int pageIndex = 1,
        int pageSize = 10,
        int firstPage = 1);
    
    Task<PagingResponse<TResult>> GetPagingListAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        int pageIndex = 1,
        int pageSize = 10,
        int firstPage = 1);

    Task<TEntity> SingleOrDefaultAsync(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
        );
    
    Task<TResult> SingleOrDefaultAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null
    );
    
    Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
    #endregion

    #region Insert Async
    
    Task InsertAsync(TEntity entity);
    
    Task InsertRangeAsync(IEnumerable<TEntity> entities);

    #endregion

    #region Update Async
    
    void UpdateAsync(TEntity entity);
    
    void UpdateRangeAsync(IEnumerable<TEntity> entities);
    
    #endregion

    #region Delete Async
    
    void Delete(TEntity entity);
    
    void DeleteRange(IEnumerable<TEntity> entities);
    
    Task<TEntity> DeleteAsync(TEntity entity);

    #endregion
    
    #region queryable

    IQueryable<TEntity> CreateBaseQuery(
        Expression<Func<TEntity, bool>> predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
        bool asNoTracking = true);
    
    #endregion
}