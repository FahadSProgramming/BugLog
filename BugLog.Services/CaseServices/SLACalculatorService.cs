using System;
using BugLog.Application.Interfaces;
using BugLog.Application.Infrastructure;

namespace BugLog.Services.CaseServices
{
    public class SLACalculatorService : ISLACalculationService
    {
        public DateTime CalculateExpectedDate(CasePriorityEnum priority) {
            return DateTime.Now;
        } 
        public DateTime? CalculateActualEndDate(CaseStatusEnum status) {
            return DateTime.Now;
        }
    }
}