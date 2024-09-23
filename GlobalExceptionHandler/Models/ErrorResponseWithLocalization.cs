namespace GlobalExceptionHandler.Models;

public class ErrorResponseWithLocalization : ErrorResponseBase
{
    public KeyValuePair<string, List<ErrorItemWithLocalization>>? Error { get; set; }
}
