using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using BugLog.Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Countries.Commands
{
    public class DeleteCountryCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Unit> {
            private readonly IBugLogDbContext _context;

            public DeleteCountryCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken) {
                var entity = await _context.Countries.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Country), request.Id);
                }

                var hasCurrencies = await _context.Currencies.AnyAsync(x => x.CountryId == request.Id);
                if(hasCurrencies) {
                    throw new DeleteFailureException(nameof(Country), request.Id, "There is a currency associated to this country. The request cannot be completed.");
                }

                _context.Countries.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}