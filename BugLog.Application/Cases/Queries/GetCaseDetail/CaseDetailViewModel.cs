using AutoMapper;
using System;
using BugLog.Application.Infrastructure;

namespace BugLog.Application.Cases.Queries
{
    public class CaseDetailViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public DateTime ActualEndDate { get; set; }
        public CaseStatusEnum Status { get; set; }
        public CasePriorityEnum Priority {get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}