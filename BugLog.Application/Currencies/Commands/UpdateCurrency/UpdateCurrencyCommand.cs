using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Currencies.Commands
{
    public class UpdateCurrencyCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double? ExchangeRate { get; set; }
        public bool? BaseCurrency { get; set; }
        public Guid? CountryId { get; set; }

        public class UpdateCurrencyCommandHandler : IRequestHandler<UpdateCurrencyCommand, Unit> {
            private readonly IBugLogDbContext _context;

            public UpdateCurrencyCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken) {
                var entity = await _context.Currencies.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Currency), request.Id);
                }

                if(request.CountryId.HasValue) {
                    var countryHasCurrency = await _context.Currencies.AnyAsync(x => x.CountryId == request.CountryId.Value);
                    if(countryHasCurrency) {
                        throw new BadRequestException("The country reference used already has a currency associated to it. The opreation cannot be completed.");
                    }
                }

                entity.Name = request.Name ?? entity.Name;
                entity.ExchangeRate = request.ExchangeRate ?? entity.ExchangeRate;
                entity.BaseCurrency = request.BaseCurrency ?? entity.BaseCurrency;
                entity.CountryId = request.CountryId ?? entity.CountryId;

                await _context.SaveChangesAsync(cancellationToken);
                
                return Unit.Value;
            }
        }
    }
}