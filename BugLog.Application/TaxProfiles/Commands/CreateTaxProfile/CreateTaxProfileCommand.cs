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
    public class CreateTaxProfileCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public TaxProfileTypeEnum TaxProfileType { get; set; }
        public Guid CurrencyId { get; set; }
        public class CreateTaxProfileCommandHandler : IRequestHandler<CreateTaxProfileCommand, Guid> {
            private readonly IBugLogDbContext _context;

            public CreateTaxProfileCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Guid> Handle(CreateTaxProfileCommand request, CancellationToken cancellationToken) {
                var hasCurrency = await _context.Currencies.AnyAsync(x => x.Id == request.CurrencyId);
                if(!hasCurrency) {
                    throw new BadRequestException("The referenced currency does not exist. The operation cannot be completed.");
                }

                var entity = new TaxProfile {
                    Name = request.Name,
                    Amount = request.Amount,
                    CurrencyId = request.CurrencyId,
                    TaxProfileType = request.TaxProfileType.GetHashCode()
                };

                await _context.TaxProfiles.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
        
    }
}