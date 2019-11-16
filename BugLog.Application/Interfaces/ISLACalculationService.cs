using System;
using BugLog.Application.Infrastructure;

namespace BugLog.Application.Interfaces
{
    public interface ISLACalculationService
    {
         DateTime CalculateExpectedDate(CasePriorityEnum priority);
         DateTime? CalculateActualEndDate(CaseStatusEnum status);
    }
}