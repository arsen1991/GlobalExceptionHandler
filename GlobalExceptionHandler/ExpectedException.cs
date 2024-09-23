using System.Net;
using Microsoft.Extensions.Logging;

namespace GlobalExceptionHandler;

public class ExpectedException : Exception
{
    public const string DefaultErrorMessage = "Unknown";

    public string ErrorKey { get; }

    public LogLevel? Severity { get; }

    public HttpStatusCode? HttpStatusCode { get; }

    public string? Description { get; }

    public string? AdditionalInfo { get; }

    public ExpectedException(string errorMessage = DefaultErrorMessage, string? description = null, string? additionalInfo = null, LogLevel? severity = null, HttpStatusCode? httpStatusCode = null)
        : base(description ?? errorMessage)
    {
        ErrorKey = errorMessage;
        Severity = severity;
        HttpStatusCode = httpStatusCode;
        Description = description;
        AdditionalInfo = additionalInfo;
    }

    public ExpectedException(Exception innerException, string errorMessage = DefaultErrorMessage, string? description = null, string? additionalInfo = null, LogLevel? severity = null, HttpStatusCode? httpStatusCode = null)
        : base(description ?? errorMessage, innerException)
    {
        ErrorKey = errorMessage;
        Severity = severity;
        HttpStatusCode = httpStatusCode;
        Description = description;
        AdditionalInfo = additionalInfo;
    }
}
