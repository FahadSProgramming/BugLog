using System.Collections.Generic;

namespace BugLog.Application.Customers.Queries
{
    public class CustomerListViewModel
    {
        public ICollection<CustomerDetailViewModel> Customers { get; set; }
        public int Count { get; set; }
    }
}