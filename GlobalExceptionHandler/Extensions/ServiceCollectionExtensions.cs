using System.Globalization;
using GlobalExceptionHandler.Handlers;
using GlobalExceptionHandler.Localization;
using GlobalExceptionHandler.Localization.Interfaces;
using GlobalExceptionHandler.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GlobalExceptionHandler.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGlobalExceptionHandlerWithLocalization(this IServiceCollection services, List<KeyValuePair<string, ErrorDescription>>? errorDescriptions = null)
    {
        services.AddSingleton<IExceptionHandler, ExceptionHandlerWithLocalization>(x => new ExceptionHandlerWithLocalization(x.GetRequiredService<ILogger<ExceptionHandlerWithLocalization>>(), x.GetRequiredService<ILocalizationProvider>(), errorDescriptions));
        return services;
    }

    public static IServiceCollection AddGlobalExceptionHandlerWithLocalizationForDevelopment(this IServiceCollection services, List<KeyValuePair<string, ErrorDescription>>? errorDescriptions = null)
    {
        services.AddSingleton<IExceptionHandler, ExceptionHandlerWithLocalizationForDevelopment>(x => new ExceptionHandlerWithLocalizationForDevelopment(x.GetRequiredService<ILogger<ExceptionHandlerWithLocalizationForDevelopment>>(), x.GetRequiredService<ILocalizationProvider>(), errorDescriptions));
        return services;
    }

    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services, List<KeyValuePair<string, ErrorDescription>>? errorDescriptions = null)
    {
        services.AddSingleton<IExceptionHandler, Handlers.ExceptionHandler>(x => new Handlers.ExceptionHandler(x.GetRequiredService<ILogger<Handlers.ExceptionHandler>>(), errorDescriptions));
        return services;
    }

    public static IServiceCollection AddGlobalExceptionHandlerForDevelopment(this IServiceCollection services, List<KeyValuePair<string, ErrorDescription>>? errorDescriptions = null)
    {
        services.AddSingleton<IExceptionHandler, ExceptionHandlerForDevelopment>(x => new ExceptionHandlerForDevelopment(x.GetRequiredService<ILogger<Handlers.ExceptionHandlerForDevelopment>>(), errorDescriptions));
        return services;
    }

    public static IServiceCollection AddLocalizationProivderWithResourceFile(this IServiceCollection services, Type resource, IEnumerable<CultureInfo>? cultureInfos = null)
    {
        services.AddSingleton<ILocalizationProvider>(new LocalizationProviderWithResourceFile(resource, cultureInfos));
        return services;
    }
}
