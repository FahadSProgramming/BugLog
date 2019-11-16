using System;
using System.Collections.Generic;

namespace BugLog.Domain.Entities
{
    public class TaxProfile : BaseEntity
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public int TaxProfileType { get; set; }
        public Currency Currency { get; set; }
        public Guid CurrencyId { get; set; }

        public ICollection<ServiceContract> ServiceContracts { get; set; }
        public ICollection<ServiceContractLine> ServiceContractLines { get; set; }
    }
}