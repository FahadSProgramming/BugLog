using System;
using System.Collections.Generic;

namespace BugLog.Domain.Entities
{
    public class PriceListItem : BaseEntity
    {
        public PriceList PriceList { get; set; }
        public Guid PriceListId { get; set; }
        public Product Product { get; set; }
        public Guid ProductId { get; set; }
        public double ItemPrice { get; set; }
        public ICollection<ServiceContractLine> ServiceContractLines { get; set; }
    }
}