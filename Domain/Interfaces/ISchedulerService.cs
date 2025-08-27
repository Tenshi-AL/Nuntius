using Domain.Models;

namespace Domain.Interfaces;

/// <summary>
/// Interface responsible for scheduling, removing, and displaying jobs.
/// It does not send messages itself — it only schedules them!
/// </summary>
public interface ISchedulerService
{
    Task ScheduleAsync(TaskSetting setting);
    Task<List<JobDetail>?> GetScheduledJobListAsync();
    Task RemoveJob(string name);
}
