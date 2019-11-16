using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using BugLog.Application.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.ServiceContracts.Commands
{
    public class CreateServiceContractCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid PriceListId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? TaxProfileId { get; set; }
        public ServiceContractStatusEnum Status { get; set; }

        public class CreateServiceContractCommandHandler : IRequestHandler<CreateServiceContractCommand, Guid> {
            private readonly IBugLogDbContext _context;

            public CreateServiceContractCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Guid> Handle(CreateServiceContractCommand request, CancellationToken cancellationToken) {

                if(request.TaxProfileId.HasValue) {
                    var hasTaxProfile = await _context.TaxProfiles.AnyAsync(x => x.Id == request.TaxProfileId.Value);
                    if(!hasTaxProfile) {
                        throw new BadRequestException("The referenced tax profile does not exist. The opration cannot be completed.");
                    }
                }

                var hasPriceList = await _context.PriceLists.AnyAsync(x => x.Id == request.PriceListId);
                if(!hasPriceList) {
                    throw new BadRequestException("The referenced price list does not eixst. The operation cannot be completed.");
                }

                var hasCustomer = await _context.Customers.AnyAsync(x => x.Id == request.CustomerId);
                if(!hasCustomer) {
                    throw new BadRequestException("The referenced customer does not exist. The operation cannot be completed.");
                }

                var entity = new ServiceContract {
                    Name = request.Name,
                    Amount = request.Amount,
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    PriceListId = request.PriceListId,
                    TaxProfileId = request.TaxProfileId,
                    CustomerId = request.CustomerId,
                    Status = request.Status.GetHashCode()
                };

                await _context.ServiceContracts.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }

    }
}