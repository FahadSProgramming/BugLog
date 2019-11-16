using System.Collections.Generic;

namespace BugLog.Application.Products.Queries
{
    public class ProductListViewModel
    {
        public ICollection<ProductDetailViewModel> Products { get; set; }
        public int Count { get; set; }
    }
}