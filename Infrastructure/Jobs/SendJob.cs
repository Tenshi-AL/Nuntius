using System.Text.Json;
using Domain.Models;
using Microsoft.Extensions.Logging;
using Quartz;
using TL;
using WTelegram;

public class SendJob(Client client, ILogger<SendJob> logger): IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        //  1. Get the job data and extract all chats
        var key = context.JobDetail.Key;
        var jobDataMap = context.JobDetail.JobDataMap;

        var jsonSettingString = jobDataMap.GetString(key.Name);
        var setting = JsonSerializer.Deserialize<TaskSetting>(jsonSettingString);
        var (name, cron, text, image, imageName, chats) = setting;

        //  1.5. Retrieve all user chats
        var allUserChats = await client.Messages_GetAllChats();

        //  2. Iterate through all chats for this job
        foreach (var chat in chats)
        {
            try
            {
                logger.LogDebug("Sending started");

                //  3. Get the current chat
                InputPeer peer = allUserChats.chats[chat.ChatId];

                //  4. Prepare the image
                InputFileBase? inputFileBase = null;
                if (image != null)
                {
                    using var ms = new MemoryStream(image);
                    inputFileBase = await client.UploadFileAsync(ms, imageName);
                }
                
                //  5. Check: Is this a regular chat or a forum topic?
                if (chat.TopicId is null)
                {
                    if (inputFileBase is not null) await client.SendMediaAsync(peer, text, inputFileBase);
                    else await client.SendMessageAsync(peer, text);
                }
                else
                {
                    if (inputFileBase is not null)
                        await client.SendMediaAsync(peer, text, inputFileBase, reply_to_msg_id: (int)chat.TopicId);
                    else await client.SendMessageAsync(peer, text, reply_to_msg_id: (int)chat.TopicId);
                }

                logger.LogDebug("Sending finished");
                await Task.Delay(1000);
            }
            catch (Exception e)
            {
                logger.LogError(e,"Error while sending.");
            }
        }
    }
}
