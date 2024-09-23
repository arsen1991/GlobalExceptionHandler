using GlobalExceptionHandler.Localization.Interfaces;
using GlobalExceptionHandler.Models;
using Microsoft.Extensions.Logging;

namespace GlobalExceptionHandler.Handlers;

public class ExceptionHandlerWithLocalizationForDevelopment : ExceptionHandlerWithLocalization
{
    public ExceptionHandlerWithLocalizationForDevelopment(ILogger<ExceptionHandlerWithLocalizationForDevelopment> logger, ILocalizationProvider validationLocalizationService, List<KeyValuePair<string, ErrorDescription>>? errorDescriptions = null)
        : base(logger, validationLocalizationService, errorDescriptions)
    {
    }

    protected override string GenerateInternalErrorMessage(Exception exception) => exception.Message;
}
