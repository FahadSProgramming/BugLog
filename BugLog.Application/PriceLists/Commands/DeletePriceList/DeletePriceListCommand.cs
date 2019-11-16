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
    public class DeletePriceListCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeletePriceListCommandHandler : IRequestHandler<DeletePriceListCommand, Unit> {
            private readonly IBugLogDbContext _context;

            public DeletePriceListCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(DeletePriceListCommand request, CancellationToken cancellationToken) {
                var entity = await _context.PriceLists.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(PriceList), request.Id);
                }

                var hasServiceContracts = await _context.ServiceContracts.AnyAsync(x => x.PriceListId == request.Id);
                if(hasServiceContracts) {
                    throw new DeleteFailureException(nameof(PriceList), request.Id, "There are service contracts associated to this price list. The operation cannot be completed.");
                } 

                var hasServiceContractLines = await _context.ServiceContractLines.AnyAsync(x => x.PriceListId == request.Id);
                if(hasServiceContractLines) {
                    throw new DeleteFailureException(nameof(PriceList), request.Id, "There are service contract items associated to this price list. The operation cannot be completed.");
                }

                var hasProducts = await _context.Products.AnyAsync(x => x.DefaultPriceListId == request.Id);
                if(hasProducts) {
                    throw new DeleteFailureException(nameof(PriceList), request.Id, "There are products associated to this price list. The operation cannot be completed.");
                }

                _context.PriceLists.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}