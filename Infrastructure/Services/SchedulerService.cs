using System.Text.Json;
using Domain.Interfaces;
using Domain.Models;
using Quartz;
using Quartz.Impl.Matchers;

namespace Infrastructure.Services;

public class SchedulerService: ISchedulerService
{
    private readonly ISchedulerFactory SchedulerFactory;
    private readonly IScheduler Scheduler;
    
    public SchedulerService(ISchedulerFactory schedulerFactory)
    {
        SchedulerFactory = schedulerFactory;
        Scheduler = SchedulerFactory.GetScheduler().Result;
    }

    public async Task RemoveJob(string name)
    {
        var triggerKey = new JobKey(name);
        await Scheduler.DeleteJob(triggerKey);
    }
    
    public async Task<List<JobDetail>?> GetScheduledJobListAsync()
    {
        var list = new List<JobDetail>();
        var jobKeys = await Scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
    
        foreach (var jobKey in jobKeys)
        {
            var jobDetail = await Scheduler.GetJobDetail(jobKey);
            if (jobDetail is null) return null;
            
            var triggers = await Scheduler.GetTriggersOfJob(jobKey);
            foreach (var trigger in triggers)
            {
                list.Add(new JobDetail(jobKey.Name,
                    trigger.GetNextFireTimeUtc()?.ToLocalTime().ToString() ?? "Not scheduled",
                    jobKey.Group,
                    trigger.Key.Name,
                    jobDetail.JobDataMap.GetString(jobKey.Name)));
            }
        }

        return list;
    }
    
    public async Task ScheduleAsync(TaskSetting setting)
    {
        var jsonModel = JsonSerializer.Serialize(setting);
        
        //create keys
        var jobKey = setting.Name;

        //create job
        var job = JobBuilder.Create<SendJob>()
            .WithIdentity(jobKey)
            .UsingJobData(jobKey, jsonModel)
            .Build();
        
        //create trigger
        var triggerKey = Guid.NewGuid().ToString();
        var trigger = TriggerBuilder.Create()
            .WithIdentity(triggerKey)
            .StartNow()
            .WithCronSchedule(setting.CronExpression, cron => { cron.InTimeZone(TimeZoneInfo.Local); })
            .Build();

        await Scheduler.ScheduleJob(job, trigger);
    }
}