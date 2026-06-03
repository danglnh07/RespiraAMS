using BuildingBlocks.Dtos;
using BuildingBlocks.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Middlewares;

public class ExceptionHandler(ILogger<ExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError("Exception: {exception}", new
        {
            exception.Message,
            context.Request.Path,
            Trace = exception.StackTrace
        });

        // Identify which exception type to handle
        var (detail, status) = exception switch
        {
            NotFoundException => (
                exception.Message,
                context.Response.StatusCode = StatusCodes.Status404NotFound
            ),
            BadRequestException => (
                exception.Message,
                context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            ValidationException e => (
                $"{exception.Message}: {string.Join(", ", e.Errors.Select(x => x.ErrorMessage))}",
                context.Response.StatusCode = StatusCodes.Status400BadRequest
            ),
            // Custom InternalServerErrorException and any uncaught exception will be 500
            _ => (
                exception.Message,
                context.Response.StatusCode = StatusCodes.Status500InternalServerError
            )
        };

        // Create ApiResponse
        var resp = ApiResponse.Fail(detail, status);

        // Write problem details into Response object
        await context.Response.WriteAsJsonAsync(resp, cancellationToken);
        return true;
    }
}