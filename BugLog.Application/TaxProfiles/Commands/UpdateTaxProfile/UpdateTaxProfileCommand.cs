using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Infrastructure;

namespace BugLog.Application.TaxProfiles.Commands
{
    public class UpdateTaxProfileCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public TaxProfileTypeEnum? TaxProfileType { get; set; }
        public Guid? CurrencyId { get; set; }
        public double? Amount { get; set; }

        public class UpdateTaxProfileCommandHandler : IRequestHandler<UpdateTaxProfileCommand, Unit> {
            private readonly IBugLogDbContext _context;
            
            public UpdateTaxProfileCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateTaxProfileCommand request, CancellationToken cancellationToken) {
                var entity = await _context.TaxProfiles
                .Include(x => x.ServiceContractLines)
                .SingleOrDefaultAsync(x => x.Id == request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(TaxProfile), request.Id);
                }

                if(request.CurrencyId.HasValue && request.CurrencyId.Value != entity.CurrencyId) {
                    var currencyExists = await _context.Currencies.AnyAsync(x => x.Id == request.CurrencyId.Value);
                    if(!currencyExists) {
                        throw new BadRequestException("The referenced currency does not exist. The operation cannot be completed.");
                    }

                    entity.CurrencyId = request.CurrencyId.Value;
                }

                if(!string.IsNullOrEmpty(request.Name)) {
                    entity.Name = request.Name;
                }

                if(request.Amount.HasValue) {
                    entity.Amount = request.Amount.Value;
                }

                if(request.TaxProfileType.HasValue) {
                    entity.TaxProfileType = request.TaxProfileType.Value.GetHashCode();
                }

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}