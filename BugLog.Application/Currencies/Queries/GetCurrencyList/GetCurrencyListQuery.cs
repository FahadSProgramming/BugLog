using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using System.Collections.Generic;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Currencies.Queries
{
    public class GetCurrencyListQuery : IRequest<CurrencyListViewModel>
    {
        public class GetCurrencyListQueryHandler : IRequestHandler<GetCurrencyListQuery, CurrencyListViewModel> {
            private readonly IBugLogDbContext _context;
            private readonly IMapper _mapper;

            public GetCurrencyListQueryHandler(IBugLogDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CurrencyListViewModel> Handle(GetCurrencyListQuery request, CancellationToken cancellationToken) {
                var entityList = await _context.Currencies
                .Include(x => x.PriceLists)
                .ToListAsync();

                var vm = new CurrencyListViewModel {
                    Currencies = _mapper.Map<List<Currency>, List<CurrencyDetailViewModel>>(entityList),
                    Count = entityList.Count
                };

                return vm;
            }
        }
    }
}