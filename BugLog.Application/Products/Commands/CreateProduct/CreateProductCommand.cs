using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Infrastructure;

namespace BugLog.Application.Products.Commands
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public ProductTypeEnum ProductType { get; set; }
        public double ListPrice { get; set; }
        public Guid DefaultPriceListId { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid> {
            private readonly IBugLogDbContext _context;

            public CreateProductCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken) {
                
                var priceListExists = await _context.PriceLists.AnyAsync(x => x.Id == request.DefaultPriceListId);
                if(!priceListExists) {
                    throw new BadRequestException("The referenced price list does not eixst. The opration cannot be completed.");
                }

                var entity = new Product {
                    Name = request.Name,
                    ProductType = request.ProductType.GetHashCode(),
                    ListPrice = request.ListPrice,
                    DefaultPriceListId = request.DefaultPriceListId
                };

                await _context.Products.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }

    }
}