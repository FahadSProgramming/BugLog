using System;
using System.Collections.Generic;

namespace BugLog.Application.PriceLists.Queries
{
    public class PriceListDetailViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CurrencyId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        //public ICollection<ServiceContractDetailViewModel> ServiceContracts { get; set; }
        //public ICollection<ProductDetailViewModel> Products { get; set; }
    }
}