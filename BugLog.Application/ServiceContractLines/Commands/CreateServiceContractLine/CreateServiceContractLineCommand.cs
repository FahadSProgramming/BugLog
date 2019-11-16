using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using BugLog.Application.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BugLog.Application.ServiceContractLines.Commands
{
    public class CreateServiceContractLineCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public double Discount { get; set; }
        public int Quantity { get; set; }
        public DiscountTypeEnum DiscountType { get; set; }
        public Guid PriceListItemId { get; set; }
        public Guid? TaxProfileId { get; set; }
        public Guid PriceListId { get; set; }
        public Guid ServiceContractId { get; set; }

        public class CreateServiceContractLineCommandHandler : IRequestHandler<CreateServiceContractLineCommand, Guid> {
            private readonly IBugLogDbContext _context;

            public CreateServiceContractLineCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Guid> Handle(CreateServiceContractLineCommand request, CancellationToken cancellationToken) {

                var hasServiceContract = await _context.ServiceContracts.AnyAsync(x => x.Id == request.ServiceContractId);
                if(!hasServiceContract) {
                    throw new BadRequestException("The referenced service contract does not exist. The operation cannot be completed.");
                }

                // var hasProduct = await _context.Products.AnyAsync(x => x.Id == request.ProductId);
                // if(!hasProduct) {
                //     throw new BadRequestException("The referenced product does not exist or is not active. The operaiton cannot be completed.");
                // }

                var hasPriceList = await _context.PriceLists.AnyAsync(x => x.Id == request.PriceListId);
                if(!hasPriceList) {
                    throw new BadRequestException("The referenced price list does not exist. The operation cannot be completed.");
                }

                var productPriceListItem = await _context.PriceListItems.AnyAsync(x => x.Id == request.PriceListItemId);
                //.Where(x => x.PriceListId == request.PriceListId && x.ProductId == request.ProductId).FirstOrDefaultAsync();

                if(!productPriceListItem) {
                    throw new BadRequestException("The referenced product does not belong to the referenced price list. The operation cannot be completed.");
                }

                if(request.TaxProfileId.HasValue) {
                    var hastaxProfile = await _context.TaxProfiles.AnyAsync(x => x.Id == request.TaxProfileId.Value);
                    if(!hastaxProfile) {
                        throw new BadRequestException("The referenced tax profile does not exist. The operation cannot be completed.");
                    }
                }

                var entity = new ServiceContractLine {
                    Name = request.Name,
                    UnitPrice = 0.00,
                    NetPrice = 0.00,
                    Discount = request.Discount,
                    DiscountType = request.DiscountType.GetHashCode(),
                    Quantity = request.Quantity,
                    PriceListItemId = request.PriceListItemId,
                    TaxProfileId = request.TaxProfileId,
                    PriceListId = request.PriceListId,
                    ServiceContractId = request.ServiceContractId
                };

                await _context.ServiceContractLines.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;

            }
        }
    }
}