using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;

namespace BugLog.Application.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit> {
            private readonly IBugLogDbContext _context;

            public DeleteCustomerCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken) {
                var entity = await _context.Customers.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Customer), request.Id);
                }
                
                var hasServiceContract = await _context.ServiceContracts.AnyAsync(x => x.CustomerId == request.Id);
                if(hasServiceContract) {
                    throw new DeleteFailureException(nameof(Customer), request.Id, "There are service contracts associated to this customer. The operation cannot be completed.");
                }

                var hasCases = await _context.Cases.AnyAsync(x => x.CustomerId == request.Id);
                if(hasCases) {
                    throw new DeleteFailureException(nameof(Customer), request.Id, "There are cases associated to this customer. The operation cannot be completed.");
                }

                var hasContacts = await _context.Contacts.AnyAsync(x => x.CustomerId == request.Id);
                if(hasContacts) {
                    throw new DeleteFailureException(nameof(Customer), request.Id, "There are contacts associated to this customer. The opration cannot be completed.");
                }

                _context.Customers.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}