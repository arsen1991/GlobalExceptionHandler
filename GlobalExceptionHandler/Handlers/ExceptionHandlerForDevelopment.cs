using GlobalExceptionHandler.Models;
using Microsoft.Extensions.Logging;

namespace GlobalExceptionHandler.Handlers;

public class ExceptionHandlerForDevelopment : ExceptionHandler
{
    public ExceptionHandlerForDevelopment(ILogger<ExceptionHandlerForDevelopment> logger, List<KeyValuePair<string, ErrorDescription>>? errorDescriptions = null)
        : base(logger, errorDescriptions)
    {
    }

    protected override string GenerateInternalErrorMessage(Exception exception) => exception.Message;
}
