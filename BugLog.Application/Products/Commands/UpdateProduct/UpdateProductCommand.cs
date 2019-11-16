using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Infrastructure;

namespace BugLog.Application.Products.Commands
{
    public class UpdateProductCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ProductTypeEnum? ProductType { get; set; }
        public double? ListPrice { get; set; }
        public Guid? DefaultPriceListId { get; set; }

        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit> {
            private readonly IBugLogDbContext _context;
            public UpdateProductCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken) {
                var entity = await _context.Products.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Product), request.Id);
                }

                if(request.DefaultPriceListId.HasValue) {

                    var priceListExists = await _context.PriceLists.AnyAsync(x => x.Id == request.DefaultPriceListId.Value);
                    if(!priceListExists) {
                        throw new BadRequestException("The referenced price list does not exist. The opration cannot be completed.");
                    }
                }

                entity.Name = request.Name ?? entity.Name;
                
                if(request.ProductType.HasValue)
                    entity.ProductType = request.ProductType.GetHashCode();
                
                if(request.ListPrice.HasValue)
                    entity.ListPrice = request.ListPrice.Value;
                
                if(request.DefaultPriceListId.HasValue)
                    entity.DefaultPriceListId = request.DefaultPriceListId.Value;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}