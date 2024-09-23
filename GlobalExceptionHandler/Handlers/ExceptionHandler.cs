using System.Collections.Frozen;
using System.Net;
using System.Net.Mime;
using GlobalExceptionHandler.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GlobalExceptionHandler.Handlers;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;
    private readonly FrozenDictionary<string, ErrorDescription>? _descriptions;

    public ExceptionHandler(ILogger<ExceptionHandler> logger, List<KeyValuePair<string, ErrorDescription>>? errorDescriptions = null)
    {
        _logger = logger;
        _descriptions = errorDescriptions?.ToFrozenDictionary();
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception == null)
        {
            return false;
        }

        if (exception is ExpectedException expectedException)
        {
            var errorKey = expectedException.ErrorKey;

            ErrorDescription? errorDescription = null;
            _descriptions?.TryGetValue(errorKey, out errorDescription);

            var message = expectedException.Description ?? errorDescription?.Description ?? errorKey;

            var response = new ErrorResponse
            {
                Message = message,
                Error = errorKey,
                AdditionalInfo = expectedException.AdditionalInfo,
            };

            httpContext.Response.ContentType = MediaTypeNames.Application.ProblemJson;

            httpContext.Response.StatusCode = (int)(expectedException.HttpStatusCode ?? errorDescription?.HttpStatusCode ?? HttpStatusCode.BadRequest);

            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            var severity = expectedException.Severity ?? errorDescription?.Severity ?? LogLevel.Warning;
        }
        else
        {
            _logger.LogError(exception, exception.Message);

            var message = GenerateInternalErrorMessage(exception);
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(
                new
                {
                    Message = message,
                },
                cancellationToken);
        }

        return true;
    }

    protected virtual string GenerateInternalErrorMessage(Exception exception) =>
        "An error occurred while processing your request.";
}
