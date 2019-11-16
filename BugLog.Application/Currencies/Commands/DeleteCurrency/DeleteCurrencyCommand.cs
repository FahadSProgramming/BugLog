using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Currencies.Commands
{
    public class DeleteCurrencyCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteCurrencyCommandHandler : IRequestHandler<DeleteCurrencyCommand, Unit> {
            private readonly IBugLogDbContext _context;

            public DeleteCurrencyCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCurrencyCommand request, CancellationToken cancellationToken) {
                var entity = await _context.Currencies.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Currency), request.Id);
                }

                var hasPriceList = await _context.PriceLists.AnyAsync(x => x.CurrencyId == request.Id);

                if(hasPriceList) {
                    throw new DeleteFailureException(nameof(Currency), request.Id, "There are price lists associated to this currency. The operation cannot be completed.");
                }

                _context.Currencies.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;

            }
        }
    }
}