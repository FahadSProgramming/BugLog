using System;

namespace BugLog.Domain.Entities
{
    public class Case : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int Priority { get; set; }
        public ServiceContract ServiceContract { get; set; }
        public Guid ServiceContractId { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public Contact Contact { get; set; }
        public Guid? ContactId { get; set; }
    }
}