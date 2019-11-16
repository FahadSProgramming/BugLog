
using System.Text.Json.Serialization;

namespace BugLog.Application.Infrastructure
{
    public enum CaseStatusEnum
    {
        Draft = 1,
        Active = 2,
        InProgress = 3,
        OnHold = 4,
        Canceled = 5,
        Resolved = 6
    }
    public enum  CasePriorityEnum {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }
}