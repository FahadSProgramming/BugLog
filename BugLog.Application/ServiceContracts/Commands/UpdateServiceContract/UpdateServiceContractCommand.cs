using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Infrastructure;

namespace BugLog.Application.ServiceContracts.Commands
{
    public class UpdateServiceContractCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? PriceListId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? TaxProfileId { get; set; }
        public ServiceContractStatusEnum? Status { get; set; }
        public class UpdateServiceContractCommandHandler : IRequestHandler<UpdateServiceContractCommand, Unit> {
            private readonly IBugLogDbContext _context;

            public UpdateServiceContractCommandHandler(IBugLogDbContext context) {
                _context = context;
            }
            public async Task<Unit> Handle(UpdateServiceContractCommand request, CancellationToken cancellationToken) {

                var entity = await _context.ServiceContracts.FindAsync(request.Id);
                if(entity == null) {
                    throw new EntityNotFoundException(nameof(ServiceContract), request.Id);
                }

                if(request.TaxProfileId.HasValue) {
                    var hasTaxProfile = await _context.TaxProfiles.AnyAsync(x => x.Id == request.TaxProfileId.Value);
                    if(!hasTaxProfile) {
                        throw new BadRequestException("The referenced tax profile does not exist. The opration cannot be completed.");
                    }
                }

                if(request.PriceListId.HasValue) {
                    var hasPriceList = await _context.PriceLists.AnyAsync(x => x.Id == request.PriceListId);
                    if(!hasPriceList) {
                        throw new BadRequestException("The referenced price list does not eixst. The operation cannot be completed.");
                    }
                }

                if(request.CustomerId.HasValue) {
                    var hasCustomer = await _context.Customers.AnyAsync(x => x.Id == request.CustomerId);
                    if(!hasCustomer) {
                        throw new BadRequestException("The referenced customer does not exist. The operation cannot be completed.");
                    }
                }

                if(!string.IsNullOrEmpty(request.Name)) {
                    entity.Name = request.Name;
                }
                if(request.Amount.HasValue) {
                    entity.Amount = request.Amount.Value;
                }
                
                if(request.StartDate.HasValue) {
                    entity.StartDate = request.StartDate.Value;
                }

                if(request.EndDate.HasValue) {
                    entity.EndDate = request.EndDate.Value;
                }

                if(request.PriceListId.HasValue) {
                    entity.PriceListId = request.PriceListId.Value;
                }

                if(request.CustomerId.HasValue) {
                    entity.CustomerId = request.CustomerId.Value;
                }

                if(request.TaxProfileId.HasValue) {
                    entity.TaxProfileId = request.TaxProfileId.Value;
                }

                if(request.Status.HasValue) {
                    entity.Status = request.Status.GetHashCode();
                }
                
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}