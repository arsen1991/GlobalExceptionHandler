namespace GlobalExceptionHandler.Models;

public class ErrorItemWithLocalization : ErrorItem
{
    public Dictionary<string, string>? ErrorTranslations { get; set; }
}
