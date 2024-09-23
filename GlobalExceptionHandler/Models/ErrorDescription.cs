using System.Net;
using Microsoft.Extensions.Logging;

namespace GlobalExceptionHandler.Models;

public class ErrorDescription
{
    public string Description { get; }

    public LogLevel Severity { get; }

    public HttpStatusCode HttpStatusCode { get; set; }

    public ErrorDescription(string description, LogLevel severity = LogLevel.Warning, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        Description = description;
        Severity = severity;
        HttpStatusCode = statusCode;
    }
}
