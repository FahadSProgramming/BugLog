using System;
using MediatR;

namespace BugLog.Application.Customers.Queries
{
    public class GetCustomerDetailQuery : IRequest<CustomerDetailViewModel>
    {
        public Guid Id { get; set; }
    }
}