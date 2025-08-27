using Domain.Interfaces;
using Domain.Models;

namespace Infrastructure.Services;

public class TaskSettingService: ITaskSettingService
{
    public required TaskSetting Setting { get; set; } = new TaskSetting();

    public void AddChat(long chatId, int? topicId) => Setting.Chats.Add(new Chat() { ChatId = chatId, TopicId = topicId });

    public void RemoveChat(long chatId, int? topicId)
    {
        var chat = Setting.Chats.FirstOrDefault(p => p.ChatId == chatId && p.TopicId is not null && p.TopicId == topicId);
        if (chat is not null) Setting.Chats.Remove(chat);
    }
}