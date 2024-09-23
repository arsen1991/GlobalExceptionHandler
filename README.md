# GlobalExceptionHandler

This is a simple global exception handler for Web API applications. It will catch all exceptions and return a JSON response with the exception message.

## Usage

### Without localization
```csharp
builder.Services.AddGlobalExceptionHandler(new List<KeyValuePair<string, GlobalExceptionHandler.Models.ErrorDescription>>
{
   // Optional. You may add your default descriptions for exceptions here.
   new KeyValuePair<string, GlobalExceptionHandler.Models.ErrorDescription> ("Test", new GlobalExceptionHandler.Models.ErrorDescription("Test error", Microsoft.Extensions.Logging.LogLevel.Error, System.Net.HttpStatusCode.Unauthorized) ),
});
```

### With localization
```csharp
builder.Services.AddGlobalExceptionHandlerWithLocalization(new List<KeyValuePair<string, GlobalExceptionHandler.Models.ErrorDescription>>
{
   // Optional. You may add your default descriptions for exceptions here.
   new KeyValuePair<string, GlobalExceptionHandler.Models.ErrorDescription> ("Test", new GlobalExceptionHandler.Models.ErrorDescription("Test error", Microsoft.Extensions.Logging.LogLevel.Error, System.Net.HttpStatusCode.Unauthorized) ),
});
```

In order to use localization with resource file, you must add the following code to your `Startup.cs` file:
```csharp
builder.Services.AddLocalizationProivderWithResourceFile(typeof(Resource), // The type of your resource file
    new List<CultureInfo>
    [
        // Your supported Cultures
    ]);
```

Or you can implement your own `ILocalizationProvider` and pass it to the `AddGlobalExceptionHandlerWithLocalization` method.

You can also use GlobalExceptionHandlers for development environments:
```csharp
builder.Services.AddGlobalExceptionHandlerForDevelopment(new List<KeyValuePair<string, GlobalExceptionHandler.Models.ErrorDescription>>
{
   // Optional. You may add your default descriptions for exceptions here.
   new KeyValuePair<string, GlobalExceptionHandler.Models.ErrorDescription> ("Test", new GlobalExceptionHandler.Models.ErrorDescription("Test error", Microsoft.Extensions.Logging.LogLevel.Error, System.Net.HttpStatusCode.Unauthorized) ),
});
```

Or
```csharp
builder.Services.AddGlobalExceptionHandlerWithLocalizationForDevelopment(new List<KeyValuePair<string, GlobalExceptionHandler.Models.ErrorDescription>>
{
   // Optional. You may add your default descriptions for exceptions here.
   new KeyValuePair<string, GlobalExceptionHandler.Models.ErrorDescription> ("Test", new GlobalExceptionHandler.Models.ErrorDescription("Test error", Microsoft.Extensions.Logging.LogLevel.Error, System.Net.HttpStatusCode.Unauthorized) ),
});
```

This will return the exception message only in development environments.

Also add this code after `app.Build()`
```csharp
app.UseExceptionHandler(_ => { });
```

To throw an exception, you can use the `ExpectedException` class:
```csharp
throw new ExpectedException("Test");
```

Also you can set the `LogLevel` and `HttpStatusCode` for each exception:
```csharp
throw new ExpectedException("Test", Microsoft.Extensions.Logging.LogLevel.Error, System.Net.HttpStatusCode.Unauthorized);
```

All ExpectedExceptions will be caught by the GlobalExceptionHandler.