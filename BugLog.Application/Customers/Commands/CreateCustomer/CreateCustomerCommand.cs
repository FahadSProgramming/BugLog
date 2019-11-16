using System.Threading;
using System.Threading.Tasks;
using System;
using MediatR;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Exceptions;

namespace BugLog.Application.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public Guid? CountryId { get; set; }
        public CustomerCategoryEnum Category { get; set; }
        public class CreateCustomerCommandHandler: IRequestHandler<CreateCustomerCommand, Guid> {
            private readonly IBugLogDbContext _context;

            public CreateCustomerCommandHandler(IBugLogDbContext context) {
                _context = context;
            }
            public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken) {

                if(request.CountryId.HasValue) {
                    var hasCountry = await _context.Countries.AnyAsync(x => x.Id == request.CountryId.Value);
                    if(!hasCountry) {
                        throw new BadRequestException("A country with referenced Id does not exist. The operation cannot be completed.");
                    }
                }

                var entity = new Customer {
                    Name = request.Name,
                    Phone = request.Phone,
                    CountryId = request.CountryId,
                    Category = request.Category.GetHashCode(),
                    IsActive = false
                };
                
                await _context.Customers.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                
                return entity.Id;
            }
        }
    }
}