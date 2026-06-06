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
        logger.LogDebug("Exception: {exception}", new
        {
            exception.TargetSite,
            exception.Message,
            context.Request.Path,
            Trace = exception.StackTrace
        });

        // Identify which exception type to handle
        string detail;
        int status;

        switch (exception)
        {
            case NotFoundException:
                detail = exception.Message;
                status = StatusCodes.Status404NotFound;
                break;
            case BadRequestException:
                detail = exception.Message;
                status = StatusCodes.Status400BadRequest;
                break;
            case ValidationException e:
                detail = string.Join(", ", e.Errors.Select(x => x.ErrorMessage));
                status = StatusCodes.Status400BadRequest;
                break;
            case InternalServerErrorException:
                logger.LogError("Internal server error: {message}", exception.Message);
                detail = exception.Message;
                status = StatusCodes.Status500InternalServerError;
                break;
            default:
                logger.LogCritical("Unexpected error occur: {exception}", new
                {
                    exception.TargetSite,
                    exception.Message,
                });
                detail = exception.Message;
                status = StatusCodes.Status500InternalServerError;
                break;
        }

        // Create ApiResponse
        var resp = ApiResponse.Fail(detail, status);

        // Write problem details into Response object
        await context.Response.WriteAsJsonAsync(resp, cancellationToken);
        return true;
    }
}