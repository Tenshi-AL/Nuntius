using Domain.Models;

namespace Domain.Interfaces;

public interface ITaskSettingService
{
    TaskSetting Setting { get; set; } 
    void AddChat(long chatId, int? topicId);
    void RemoveChat(long chatId, int? topicId);
}