using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Infrastructure;

namespace BugLog.Application.ServiceContractLines.Commands
{
    public class UpdateServiceContractLineCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? TaxProfileId { get; set; }
        public Guid? PriceListItemId { get; set; }
        public Guid? PriceListId { get; set; }
        //public Guid? ServiceContractId { get; set; }
        public int? Quantity { get; set; }
        public double? Discount { get; set; }
        public DiscountTypeEnum? DiscountType { get; set; }

        public class UpdateServiceContractLineCommandHandler : IRequestHandler<UpdateServiceContractLineCommand, Unit> {
            private readonly IBugLogDbContext _context;

            public UpdateServiceContractLineCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateServiceContractLineCommand request, CancellationToken cancellationToken) {
                
                var entity = await _context.ServiceContractLines.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(ServiceContractLine), request.Id);
                }

                if(request.TaxProfileId.HasValue) {
                    var hasTaxProfile = await _context.TaxProfiles.AnyAsync(x => x.Id == request.TaxProfileId.Value);

                    if(!hasTaxProfile) {
                        throw new BadRequestException("The referenced tax profile id does not exist. The operation cannot be completed.");
                    }
                }

                if(request.PriceListItemId.HasValue) {
                    var priceListItem = await _context.PriceListItems.Include(x => x.PriceList).SingleOrDefaultAsync(x => x.Id == request.PriceListItemId.Value);
                    if(priceListItem == null) {
                        throw new BadRequestException("The referenced price list item does not exist. The operation cannot be completed.");

                    }
                    if(request.PriceListId.HasValue && request.PriceListId.Value != priceListItem.PriceListId) {
                        throw new BadRequestException("The referenced price list item does not exist in the referenced price list. The operation cannot be completed.");
                    }
                }

                if(request.PriceListId.HasValue) {
                    var hsaPriceList = await _context.PriceLists.AnyAsync(x => x.Id == request.PriceListId.Value);
                    if(!hsaPriceList) {
                        throw new BadRequestException("The referenced price list does not exist. The operation cannot be completed.");
                    }
                }

                entity.Name = request.Name ?? entity.Name;
                if(request.Quantity.HasValue) {
                    entity.Quantity = request.Quantity.Value;
                }
                if(request.DiscountType.HasValue) {
                    entity.DiscountType = request.DiscountType.Value.GetHashCode();
                }
                if(request.Discount.HasValue) {
                    entity.Discount = request.Discount.Value;
                }

                if(request.TaxProfileId.HasValue) {
                    entity.TaxProfileId = request.TaxProfileId.Value;
                }

                if(request.PriceListId.HasValue) {
                    entity.PriceListId = request.PriceListId.Value;
                }

                if(request.PriceListItemId.HasValue) {
                    entity.PriceListItemId = request.PriceListItemId.Value;
                }

                // if(request.ServiceContractId.HasValue) {
                //     entity.ServiceContractId = request.ServiceContractId.Value;
                // }

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}