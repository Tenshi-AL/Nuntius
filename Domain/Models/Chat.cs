namespace Domain.Models;

/// <summary>
/// This is the chat model used in the scheduler settings for forwarding.
/// It is through this model that the Scheduler understands where to forward the message.
/// </summary>
public class Chat
{
    /// <summary>
    /// Chat identifier.
    /// </summary>
    public required long ChatId { get; init; }
    /// <summary>
    /// Forum topic identifier. Present only if the chat has subchats.
    /// </summary>
    public int? TopicId { get; init; }
}
