using System.Collections.Generic;

namespace BugLog.Application.Currencies.Queries
{
    public class CurrencyListViewModel
    {
        public ICollection<CurrencyDetailViewModel> Currencies { get; set; }
        public int Count { get; set; }
    }
}