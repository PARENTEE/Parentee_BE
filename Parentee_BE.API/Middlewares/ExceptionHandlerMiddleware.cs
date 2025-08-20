using Parentee_BE.DAL.Data.Exceptions;
using Parentee_BE.DAL.Data.Metadatas;

namespace Parentee_BE.Middlewares;

public class ExceptionHandlerMiddleware(
    ILogger<ExceptionHandlerMiddleware> logger,
    RequestDelegate next,
    IHostEnvironment env)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var errorId = Guid.NewGuid().ToString();
            var timeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
            LogError(errorId,timeStamp, context, ex);
            await HandleExceptionAsync(context, errorId, timeStamp, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, string errorId, string timeStamp, Exception exception)
    {
        var (statusCode, message, reason) = exception switch
        {
            ApiException apiEx =>
                ((int)apiEx.StatusCode, apiEx.ExceptionMessage, apiEx.Message),

            InvalidOperationException =>
                (StatusCodes.Status400BadRequest, "Invalid Operation", exception.Message),

            _ => (StatusCodes.Status500InternalServerError,
                "Internal Server Error",
                env.IsDevelopment() ? exception.Message : "An unexpected error occurred")
        };
        
        var errorResponse = ApiResponseBuilder.BuildErrorResponse(
            statusCode: statusCode,
            message: message,
            reason: reason,
            data: new
            {
                ErrorId = errorId,
                Timestamp = timeStamp,
                Details = GetAdditionalInfo(exception)
            }
        );
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        
        await context.Response.WriteAsJsonAsync(errorResponse);
    }
    
    private void LogError(string errorId, string timeStamp, HttpContext context, Exception exception)
    {
        var error = new
        {
            ErrorId = errorId,
            Timestamp = timeStamp,
            RequestPath = context.Request.Path,
            RequestMethod = context.Request.Method,
            ExceptionType = exception.GetType().Name,
            ExceptionMessage = exception.Message,
            StackTrace = exception.StackTrace,
            InnerException = exception.InnerException?.Message,
            User = context.User?.Identity?.Name ?? "Anonymous",
            AdditionalInfo = GetAdditionalInfo(exception)
        };

        var logLevel = exception switch
        {
            BusinessException => LogLevel.Warning,
            ValidationException => LogLevel.Warning,
            NotFoundException => LogLevel.Information,
            _ => LogLevel.Error
        };

        logger.Log(logLevel, exception,
            "Error ID: {ErrorId} - Path: {Path} - Method: {Method} - {@error}",
            errorId,
            context.Request.Path,
            context.Request.Method,
            error);
    }
    
    private object GetAdditionalInfo(Exception exception)
    {
        return exception switch
        {
            ValidationException valEx => new
            {
                ValidationDetails = valEx.Message
            },
            BusinessException busEx => new
            {
                BusinessRule = busEx.Message
            },
            NotFoundException notFoundEx => new
            {
                Entity = notFoundEx.Message
            },
            BadRequestException badRequestEx => new
            {
                BadRequest = badRequestEx.Message
            },
            _ => new { }
        };
    }
}