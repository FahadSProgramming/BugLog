using System;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using System.Collections.Generic;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace BugLog.Application.PriceLists.Queries
{
    public class GetPriceListCollectionQuery : IRequest<PriceListCollectionViewModel>
    {
        public class GetPriceListCollectionQueryHandler : IRequestHandler<GetPriceListCollectionQuery, PriceListCollectionViewModel> {
            private readonly IBugLogDbContext _context;
            private readonly IMapper _mapper;

            public GetPriceListCollectionQueryHandler(IBugLogDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PriceListCollectionViewModel> Handle(GetPriceListCollectionQuery request, CancellationToken cancellationToken) {
                var entityList = await _context.PriceLists
                .Include(x => x.Products)
                .ToListAsync(cancellationToken);

                var vm = new PriceListCollectionViewModel {
                    PriceLists = _mapper.Map<List<PriceList>, List<PriceListDetailViewModel>>(entityList),
                    Count = entityList.Count
                };

                return vm;
            }
        }
    }
}