using System;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using System.Collections.Generic;

namespace BugLog.Application.Customers.Queries
{
    public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, CustomerListViewModel>
    {
        private readonly IBugLogDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerListQueryHandler(IBugLogDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerListViewModel> Handle(GetCustomerListQuery request, CancellationToken cancellationToken) {
            
            var entityList = await _context.Customers
            .Include(x => x.Contacts)
            .Include(x => x.Cases)
            .ToListAsync(cancellationToken);

            var vm = new CustomerListViewModel {
                Customers =_mapper.Map<List<Customer>, List<CustomerDetailViewModel>>(entityList),
                Count = entityList.Count
            };

            return vm;
        }
    }
}