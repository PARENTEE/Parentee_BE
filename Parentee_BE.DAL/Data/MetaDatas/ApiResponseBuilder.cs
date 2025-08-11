namespace Parentee_BE.DAL.Data.Metadatas;

public class ApiResponseBuilder
{
    public static ApiResponse<TResponse> BuildResponse<TResponse>(int statusCode, bool isSuccess, string message, TResponse data, string? reason = null)
    {
        return new ApiResponse<TResponse>
        {
            StatusCode = statusCode,
            IsSuccess = isSuccess,
            Message = message,
            Data = data,
            Reason = reason
        };
    }
    
    public static ApiResponse<TResponse> BuildErrorResponse<TResponse>(int statusCode, string message, TResponse data, string? reason = null)
    {
        return new ApiResponse<TResponse>
        {
            StatusCode = statusCode,
            IsSuccess = false,
            Message = message,
            Data = data,
            Reason = reason
        };
    }
    
    public static ApiResponse<PagingResponse<T>> BuildPageResponse<T>(
        IEnumerable<T> items,
        int totalPages,
        int currentPage,
        int pageSize,
        long totalItems,
        string message)
    {
        var pagedResponse = new PagingResponse<T>
        {
            Items = items,
            Meta = new PaginationMeta
            {
                TotalPages = totalPages,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalItems = totalItems
            }
        };

        return new ApiResponse<PagingResponse<T>>
        {
            Data = pagedResponse,
            Message = message,
            StatusCode = 200,
            IsSuccess = true,
            Reason = null
        };
    }
}