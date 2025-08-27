using System.ComponentModel.DataAnnotations;
using Domain.Attributes;

namespace Domain.Models;


public class TaskSetting
{
    [Required] public string? Name { get; set; }
    [Required, CronValidation(ErrorMessage = "Cron error")] public string? CronExpression { get; set; }
    [Required] public string? Text { get; set; }
    public string? ImageName { get; set; }
    public byte[]? Image { get; set; }
    public List<Chat> Chats { get;  set; } = [];

    public void Deconstruct(out string? name, out string? cronExpression, out string? text, out byte[]? image,
        out string? imageName, out List<Chat> chats)
    {
        name = Name;
        cronExpression = CronExpression;
        text = Text;
        image = Image;
        imageName = ImageName;
        chats = Chats;
    }
}