using System;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.PriceLists.Commands
{
    public class CreatePriceListCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid CurrencyId { get; set; }

        public class CreatePriceListCommandHandler : IRequestHandler<CreatePriceListCommand, Guid> {
            private readonly IBugLogDbContext _context;

            public CreatePriceListCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Guid> Handle(CreatePriceListCommand request, CancellationToken cancellationToken) {
                
                var hasPriceListForCurrency = await _context.PriceLists.AnyAsync(x => x.CurrencyId == request.CurrencyId);
                if(hasPriceListForCurrency) {
                    throw new BadRequestException("There is already a price list for the referenced currency.");
                }
                
                var entity = new PriceList {
                    Name = request.Name,
                    CurrencyId = request.CurrencyId
                };

                await _context.PriceLists.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}