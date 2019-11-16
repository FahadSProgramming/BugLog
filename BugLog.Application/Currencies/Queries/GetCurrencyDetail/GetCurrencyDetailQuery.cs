using System;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Currencies.Queries
{
    public class GetCurrencyDetailQuery : IRequest<CurrencyDetailViewModel>
    {
        public Guid Id { get; set; }

        public class GetCurrencyDetailQueryHandler : IRequestHandler<GetCurrencyDetailQuery, CurrencyDetailViewModel> {
            private readonly IBugLogDbContext _context;
            private readonly IMapper _mapper;

            public GetCurrencyDetailQueryHandler(IBugLogDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CurrencyDetailViewModel> Handle(GetCurrencyDetailQuery request, CancellationToken cancellationToken) {
                var entity = await _context.Currencies
                .Include(x => x.PriceLists)
                .SingleOrDefaultAsync(x => x.Id == request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Currency), request.Id);
                }

                var vm = _mapper.Map<Currency, CurrencyDetailViewModel>(entity);
                return vm;
            }
        }
    }
}