using System;
using BugLog.Application.Interfaces;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Currencies.Commands
{
    public class CreateCurrencyCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public double ExchangeRate { get; set; }
        public bool BaseCurrency { get; set; }
        public Guid CountryId { get; set; }

        public class CreateCurrencyCommandHandler : IRequestHandler<CreateCurrencyCommand, Guid> {
            private readonly IBugLogDbContext _context;
            
            public CreateCurrencyCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Guid> Handle(CreateCurrencyCommand request, CancellationToken cancellationToken) {
                var entity = new Currency {
                    Name = request.Name,
                    ExchangeRate = request.ExchangeRate,
                    BaseCurrency = request.BaseCurrency,
                    CountryId = request.CountryId
                };

                await _context.Currencies.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}