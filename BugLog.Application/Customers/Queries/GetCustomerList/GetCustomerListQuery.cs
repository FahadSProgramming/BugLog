using MediatR;

namespace BugLog.Application.Customers.Queries
{
    public class GetCustomerListQuery : IRequest<CustomerListViewModel>
    {
    }
}