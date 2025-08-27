using Domain.Interfaces;
using Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;
using WTelegram;

namespace Infrastructure.Services;

/// <summary>
/// Telegram authorization service
/// </summary>
/// <param name="configuration">Configuration from appsettings.json</param>
/// <param name="client">WTelegram client</param>
public class TelegramAuthorization(IConfiguration configuration, Client client): ITelegramAuthorization
{
    public string? ConfigNeeded { get; set; }= "connecting";
    
    /// <summary>
    /// Authorization function, use in background
    /// </summary>
    /// <param name="stoppingToken">Cancellation token</param>
    /// <returns></returns>
    /// <exception cref="TelegramConfigurationException">Telegram configuration error</exception>
    public async Task AuthorizationAsync(CancellationToken stoppingToken)
    {
        var telegramBotSettings = configuration.GetSection("TelegramConfiguration") ?? throw new TelegramConfigurationException("TelegramConfiguration error. Check TelegramConfiguration section in appsettings.json");
        var phoneNumber = telegramBotSettings["PhoneNumber"] ?? throw new TelegramConfigurationException("TelegramConfiguration error. Check PhoneNumber param in appsettings.json");
        ConfigNeeded = await DoLogin(phoneNumber);
    }
    
    /// <summary>
    /// Login function, login by config needed
    /// </summary>
    /// <param name="loginInfo"></param>
    /// <returns></returns>
    public async Task<string> DoLogin(string loginInfo)
    {
        return ConfigNeeded = await client.Login(loginInfo);
    }
    
    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        client.Dispose();
    }
}