namespace Infrastructure.Exceptions;

/// <summary>
/// Telegram configuration exception when value does not exist in appsettings.json
/// </summary>
/// <param name="message">Error message</param>
public class TelegramConfigurationException(string message): Exception(message) { }