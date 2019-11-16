using System.Collections.Generic;

namespace BugLog.Application.PriceLists.Queries
{
    public class PriceListCollectionViewModel
    {
        public ICollection<PriceListDetailViewModel> PriceLists { get; set; }
        public int Count { get; set; }
    }
}