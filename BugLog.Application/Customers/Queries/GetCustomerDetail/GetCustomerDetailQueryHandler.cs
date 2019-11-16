using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Interfaces;
using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Exceptions;

namespace BugLog.Application.Customers.Queries
{
    public class GetCustomerDetailQueryHandler : IRequestHandler<GetCustomerDetailQuery, CustomerDetailViewModel>
    {
        private readonly IBugLogDbContext _context;
        private readonly IMapper _mapper;

        public GetCustomerDetailQueryHandler(IBugLogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerDetailViewModel> Handle(GetCustomerDetailQuery request, CancellationToken cancellationToken) {

            var entity = await _context.Customers
            .Include(x => x.Contacts)
            .Include(x => x.Cases)
            .SingleOrDefaultAsync(x => x.Id == request.Id);

            if(entity == null) {
                throw new EntityNotFoundException(nameof(Customer), request.Id);
            }
            var vm = _mapper.Map<Customer, CustomerDetailViewModel>(entity);

            return vm;
        }
    }
}