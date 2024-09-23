using System.Globalization;
using System.Resources;
using GlobalExceptionHandler.Localization.Interfaces;

namespace GlobalExceptionHandler.Localization;

public class LocalizationProviderWithResourceFile : ILocalizationProvider
{
    private readonly Type _resourceSource;
    private readonly IEnumerable<CultureInfo> _supportedCultures = new List<CultureInfo>
        {
            new CultureInfo("en"),
        };

    public LocalizationProviderWithResourceFile(Type resourceSource, IEnumerable<CultureInfo>? cultureInfos = null)
    {
        _resourceSource = resourceSource;
        if (cultureInfos != null)
        {
            _supportedCultures = cultureInfos;
        }
    }

    public Dictionary<string, string> GetAllTranslations(string resourceKey)
    {
        var translations = new Dictionary<string, string>();

        foreach (var culture in _supportedCultures)
        {
            var rm = new ResourceManager(_resourceSource);
            var translation = rm.GetString(resourceKey, culture);
            if (translation != null)
            {
                translations.Add(culture.Name, translation);
            }
            else
            {
                translations.Add(culture.Name, resourceKey);
            }
        }

        return translations;
    }

    public Dictionary<string, string> GetAllTranslations<T>(T key)
        where T : Enum
    {
        var enumName = key.GetType().Name;
        var enumIntValue = Convert.ToInt32(key);
        return GetAllTranslations($"{enumName}_{enumIntValue}");
    }
}
