using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using BugLog.Application.Infrastructure;

namespace BugLog.Application.TaxProfiles.Commands
{
    public class DeleteTaxProfileCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteTaxProfileCommandHandler : IRequestHandler<DeleteTaxProfileCommand, Unit> {
            private readonly IBugLogDbContext _context;
            
            public DeleteTaxProfileCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteTaxProfileCommand request, CancellationToken cancellationToken) {
                var entity = await _context.TaxProfiles.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(TaxProfile), request.Id);
                }

                _context.TaxProfiles.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
           }
        }
    }
}