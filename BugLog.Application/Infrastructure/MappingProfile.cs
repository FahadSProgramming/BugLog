using AutoMapper;
using BugLog.Application.Contacts.Queries;
using BugLog.Application.Countries.Queries;
using BugLog.Application.Currencies.Queries;
using BugLog.Application.Customers.Queries;
using BugLog.Application.PriceLists.Queries;
using BugLog.Application.Products.Queries;
using BugLog.Application.TaxProfiles.Queries;
using BugLog.Domain.Entities;

namespace BugLog.Application.Cases.Queries
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Case, CaseDetailViewModel>();
            CreateMap<Customer, CustomerDetailViewModel>();
            CreateMap<Contact, ContactDetailViewModel>();
            CreateMap<Country, CountryDetailViewModel>();
            CreateMap<Currency, CurrencyDetailViewModel>();
            CreateMap<PriceList, PriceListDetailViewModel>();
            CreateMap<Product, ProductDetailViewModel>();
            CreateMap<TaxProfile, TaxProfileDetailViewModel>();
            //  .ForMember(d => d.Status.GetHashCode(), o => o.MapFrom(s => s.Status))
            //  .ForMember(d => d.Priority.GetHashCode(), o => o.MapFrom(s => s.Priority));
        }
    }
}