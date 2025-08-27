using System.ComponentModel.DataAnnotations;
using Quartz;

namespace Domain.Attributes;

public class CronValidationAttribute: ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is string cronExpression && CronExpression.IsValidExpression(cronExpression);
    }
}