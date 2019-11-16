using System;
namespace BugLog.Application.Infrastructure
{
    public class ViewModelBase
    {
        public Guid? CreatedById { get; set; }
        public Guid? ModifiedById { get; set; }
    }
}