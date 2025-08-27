using Domain.Interfaces;

namespace UI.BackgroundServices;

/// <summary>
/// Class for initial background authorization.
/// </summary>
/// <param name="telegramAuthorization">Telegram authorization service.</param>
/// <param name="configuration">Configuration from appsettings.json.</param>
public class TelegramBackgroundAuthorizationService(
    ITelegramAuthorization telegramAuthorization, 
    IConfiguration configuration) : BackgroundService
{
    public override void Dispose()
    {
        telegramAuthorization.Dispose();
        base.Dispose();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await telegramAuthorization.AuthorizationAsync(stoppingToken);
    }
}
