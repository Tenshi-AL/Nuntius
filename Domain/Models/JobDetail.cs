namespace Domain.Models;

/// <summary>
/// This model represents all the key details of a job.
/// It is read-only and is displayed to the user.
/// </summary>
/// <param name="JobKey">The unique identifier of the job.</param>
/// <param name="JobScheduledTime">The time the job is scheduled to run.</param>
/// <param name="JobGroup">The group the job belongs to.</param>
/// <param name="JobTriggerName">The name of the trigger associated with the job.</param>
/// <param name="JobJsonDataMap">The job's data map in JSON format.</param>
public record JobDetail(
    string JobKey,
    string JobScheduledTime,
    string JobGroup,
    string JobTriggerName,
    string JobJsonDataMap);
