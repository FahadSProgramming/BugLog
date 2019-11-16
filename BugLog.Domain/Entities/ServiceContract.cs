using System;
using System.Collections.Generic;

namespace BugLog.Domain.Entities
{
    public class ServiceContract : BaseEntity
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public PriceList PriceList { get; set; }
        public Guid PriceListId { get; set; }
        public Customer Customer { get; set; }
        public Guid CustomerId { get; set; }
        public TaxProfile TaxProfile { get; set; }
        public Guid? TaxProfileId { get; set; }
        public int Status { get; set; }
        public ICollection<ServiceContractLine> ServiceContractLines { get; set; }
        public ICollection<Case> Cases { get; set; }
        
    }
}