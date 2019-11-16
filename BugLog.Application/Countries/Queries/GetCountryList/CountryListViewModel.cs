using System.Collections.Generic;

namespace BugLog.Application.Countries.Queries
{
    public class CountryListViewModel
    {
        public ICollection<CountryDetailViewModel> Countries { get; set; }
        public int Count { get; set; }
    }
}