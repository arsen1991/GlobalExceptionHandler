namespace GlobalExceptionHandler.Localization.Interfaces;

public interface ILocalizationProvider
{
    Dictionary<string, string> GetAllTranslations(string resourceKey);

    Dictionary<string, string> GetAllTranslations<T>(T key)
        where T : Enum;
}