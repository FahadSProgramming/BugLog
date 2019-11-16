using System.Collections.Generic;

namespace BugLog.Application.Cases.Queries
{
    public class CaseListViewModel
    {
        public ICollection<CaseDetailViewModel> Cases { get; set; }
        public int Count { get; set; }
    }
}