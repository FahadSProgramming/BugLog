using System;
using System.Collections.Generic;

namespace BugLog.Domain.Entities
{
    public class PriceList : BaseEntity
    {
        public string Name { get; set; }
        public Currency Currency { get; set; }
        public Guid CurrencyId { get; set; }
        public ICollection<ServiceContract> ServiceContracts { get; set; }
        public ICollection<ServiceContractLine> ServiceContractLines { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<PriceListItem> PriceListItems { get; set; }
    }
}