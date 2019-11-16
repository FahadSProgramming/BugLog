using System;
using System.Collections.Generic;

namespace BugLog.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int? ProductType { get; set; }
        public double ListPrice { get; set; }
        public PriceList DefaultPriceList { get; set; }
        public Guid DefaultPriceListId { get; set; }
        //public ICollection<ServiceContractLine> ServiceContractLines { get; set; }
        public ICollection<PriceListItem> PriceListItems { get; set; }
    }
}