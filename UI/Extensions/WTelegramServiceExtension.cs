using Infrastructure.Exceptions;

namespace UI.Extensions;

public static class WTelegramServiceExtension
{
    public static IServiceCollection AddWTelegram(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var telegramConfiguration = configuration.GetSection("TelegramConfiguration") ?? throw new TelegramConfigurationException("TelegramConfiguration error. Check TelegramConfiguration section in appsettings.json");
        var apiIdFromJson = (telegramConfiguration["ApiId"] ?? throw new TelegramConfigurationException("TelegramConfiguration error. Check ApiId param in appsettings.json"));
        var apiHash = telegramConfiguration["ApiHash"] ?? throw new TelegramConfigurationException("TelegramConfiguration error. Check ApiHash param in appsettings.json");
        
        if (!int.TryParse(apiIdFromJson, out var apiId))
            throw new Exception("TelegramConfiguration error. Check apiId param in appsettings.json. Example: \"ApiId\": 22103192,");
        
        serviceCollection.AddSingleton<WTelegram.Client>(p =>
            new WTelegram.Client(apiId, apiHash));
        
        return serviceCollection;
    }
}