using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.ServiceContracts.Commands
{
    public class DeleteServiceContractCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteServiceContractCommandHandler : IRequestHandler<DeleteServiceContractCommand, Unit> {
            private readonly IBugLogDbContext _context;

            public DeleteServiceContractCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteServiceContractCommand request, CancellationToken cancellationToken) {
                var entity = await _context.ServiceContracts.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(ServiceContract), request.Id);
                }
                
                var hasCases = await _context.Cases.AnyAsync(x => x.ServiceContractId == request.Id);
                if(hasCases) {
                    throw new DeleteFailureException(nameof(ServiceContract), request.Id, "This serivce contract has cases assocaited to it. The operation cannot be completed.");
                }

                var serviceContractLineCollection = await _context.ServiceContractLines.ToListAsync(cancellationToken);

                _context.ServiceContractLines.RemoveRange(serviceContractLineCollection);
                _context.ServiceContracts.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}