using System.Collections.Generic;

namespace BugLog.Application.TaxProfiles.Queries
{
    public class TaxProfileListViewModel
    {
        public ICollection<TaxProfileDetailViewModel> TaxProfiles { get; set; }
        public int Count { get; set; }
    }
}