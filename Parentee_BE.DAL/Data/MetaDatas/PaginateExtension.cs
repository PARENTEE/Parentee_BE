using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Metadatas;

public static class PaginateExtension
{
    public static async Task<PagingResponse<TEntity>> ToPagingResponseAsync<TEntity>(
        this IQueryable<TEntity> query,
        int pageIndex,
        int pageSize,
        int firstPage = 1)
    {
        if(firstPage > pageIndex)
            throw new ArgumentException($"Page ({pageIndex}) must be greater or equal than first page ({firstPage})");
        
        var totalItems = query.Count();
        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagingResponse<TEntity>
        {
            Items = items,
            Meta = new PaginationMeta
            {
                CurrentPage = pageIndex,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
            }
        };
    }
}