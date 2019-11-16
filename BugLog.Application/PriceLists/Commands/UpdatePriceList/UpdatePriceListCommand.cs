using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.PriceLists.Commands
{
    public class UpdatePriceListCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? CurrencyId { get; set; }

        public class UpdatePriceListCommandHandler : IRequestHandler<UpdatePriceListCommand, Unit> {
            private readonly IBugLogDbContext _context;

            public UpdatePriceListCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(UpdatePriceListCommand request, CancellationToken cancellationToken) {
                var entity = await _context.PriceLists.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(PriceList), request.Id);
                }

                if(request.CurrencyId.HasValue) {
                    var hasPriceListWithCurrency = await _context.PriceLists.AnyAsync(x => x.CurrencyId == request.CurrencyId.Value);

                    if(hasPriceListWithCurrency) {
                        throw new BadRequestException("There is already a price list with updated currency reference. The operation cannot be completed.");
                    }
                }

                entity.Name = request.Name ?? entity.Name;
                entity.CurrencyId = request.CurrencyId.HasValue ? request.CurrencyId.Value : entity.CurrencyId;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;

            }
        }
    }
}