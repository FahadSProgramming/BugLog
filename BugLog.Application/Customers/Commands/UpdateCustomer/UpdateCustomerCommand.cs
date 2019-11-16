using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using BugLog.Application.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Customers.Commands
{
    public class UpdateCustomerCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public Guid? CountryId { get; set; }
        public bool IsActive { get; set; }
        public CustomerCategoryEnum Category { get; set; }

        public class UpdateCustomerCommandHandler: IRequestHandler<UpdateCustomerCommand, Unit> {
            private readonly IBugLogDbContext _context;
            
            public UpdateCustomerCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken) {
                var entity = await _context.Customers.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Customer), request.Id);
                }

                if(request.CountryId.HasValue) {
                    var hasCountry = await _context.Countries.AnyAsync(x => x.Id == request.CountryId.Value);
                    if(!hasCountry) {
                        throw new BadRequestException("The referenced country does not exist. The operation cannot be completed.");
                    }
                }

                entity.Name = request.Name;
                entity.Category = request.Category.GetHashCode();
                entity.Phone = request.Phone;
                entity.IsActive = request.IsActive;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }
}