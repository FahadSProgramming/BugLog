using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Products.Commands
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit> {
            private readonly IBugLogDbContext _context;

            public DeleteProductCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken) {
                var entity = await _context.Products.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Product), request.Id);
                }

                var hasPriceListItems = await _context.PriceListItems.AnyAsync(x => x.ProductId == request.Id);

                if(hasPriceListItems) {
                    throw new DeleteFailureException(nameof(Product), request.Id, "There are price list items associated to this product. The opreation cannot be completed.");
                }

                _context.Products.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}