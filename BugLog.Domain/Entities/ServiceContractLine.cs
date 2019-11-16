using System;

namespace BugLog.Domain.Entities
{
    public class ServiceContractLine : BaseEntity
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public double NetPrice { get; set; }
        public double Discount { get; set; }
        public int DiscountType { get; set; }
        public int Quantity { get; set; }
        public PriceListItem PriceListItem { get; set; }
        public Guid PriceListItemId { get; set; }
        public TaxProfile TaxProfile { get; set; }
        public Guid? TaxProfileId { get; set; }
        public PriceList PriceList { get; set; }
        public Guid PriceListId { get; set; }
        public ServiceContract ServiceContract { get; set; }
        public Guid ServiceContractId { get; set; }
    }
}