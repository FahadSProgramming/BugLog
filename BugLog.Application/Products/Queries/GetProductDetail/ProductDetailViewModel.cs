using System;
using BugLog.Application.Infrastructure;

namespace BugLog.Application.Products.Queries
{
    public class ProductDetailViewModel
    {
        public Guid Id { get; set; }
        public ProductTypeEnum ProductType { get; set; }
        public double ListPrice { get; set; }
        public Guid DefaultPriceListId { get; set; }
        //public ICollection<ServiceContractLineDetail> ServiceContractLines { get; set; }
    }
}