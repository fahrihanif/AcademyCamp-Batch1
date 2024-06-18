using System.Text.Json;
using API.DTOs.Responses;
using Microsoft.AspNetCore.Diagnostics;

namespace API.Utilities;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
                                                CancellationToken cancellationToken)
    {
        var (statusCode, message) = MapException(exception);

        httpContext.Response.StatusCode = statusCode;
        httpContext.Response.ContentType = "application/json";

        var serializedErrorResponse = JsonSerializer.Serialize(new MessageResponseDto(statusCode, message));
        await httpContext.Response.WriteAsync(serializedErrorResponse, cancellationToken);

        return true;
    }

    private static (int StatusCode, string Message) MapException(Exception exception)
    {
        return exception switch {
            ArgumentException => (StatusCodes.Status400BadRequest, exception.Message),
            NullReferenceException => (StatusCodes.Status404NotFound, exception.Message),
            _ => (StatusCodes.Status500InternalServerError,
                  $"Internal server error occured. Please contact the administrator. {exception.Message}")
        };
    }
}
