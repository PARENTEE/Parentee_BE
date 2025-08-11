using System.Net;

namespace Parentee_BE.DAL.Data.Exceptions;

public abstract class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public abstract string ExceptionMessage { get; }

    public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError) : base(message)
    {
        StatusCode = statusCode;
    }
}

public class NotFoundException : ApiException
{
    public override string ExceptionMessage => "Resource not found";
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound)
    {
    }
}

public class BadRequestException : ApiException
{
    public override string ExceptionMessage => "Bad Request";
    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}

public class UnauthorizedException : ApiException
{
    public override string ExceptionMessage => "Unauthorized Access";
    public UnauthorizedException(string message) : base(message, HttpStatusCode.Unauthorized)
    {
    }
}

public class BusinessException : ApiException
{
    public override string ExceptionMessage => "Business Rule Violation";
    public BusinessException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}

public class ValidationException : ApiException
{
    public override string ExceptionMessage => "Validation Error";
    public ValidationException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}

public class ForbiddenException : ApiException
{
    public override string ExceptionMessage => "Validation Error";
    public ForbiddenException(string message) : base(message, HttpStatusCode.Forbidden)
    {
    }
}

