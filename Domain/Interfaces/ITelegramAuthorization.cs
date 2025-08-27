namespace Domain.Interfaces;

/// <summary>
/// Telegram authorization interface
/// </summary>
public interface ITelegramAuthorization
{
    /// <summary>
    /// Needed auth entity
    /// </summary>
    string? ConfigNeeded { get; set; }
    /// <summary>
    /// Authorization function, use in background
    /// </summary>
    /// <param name="stoppingToken">Cancellation token</param>
    /// <returns></returns>
    Task AuthorizationAsync(CancellationToken stoppingToken);
    /// <summary>
    /// Login function, login by config needed
    /// </summary>
    /// <param name="loginInfo"></param>
    /// <returns></returns>
    Task<string> DoLogin(string loginInfo);
    /// <summary>
    /// Dispose
    /// </summary>
    void Dispose();
}