using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BugLog.Application.TaxProfiles.Queries
{
    public class GetTaxProfileListQuery : IRequest<TaxProfileListViewModel>
    {
        public class GetTaxProfileListQueryHandler : IRequestHandler<GetTaxProfileListQuery, TaxProfileListViewModel> {
            private readonly IBugLogDbContext _context;
            private readonly IMapper _mapper;

            public GetTaxProfileListQueryHandler(IBugLogDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<TaxProfileListViewModel> Handle(GetTaxProfileListQuery request, CancellationToken cancellationToken) {
                var entityList = await _context.TaxProfiles
                .Include(x => x.Currency)
                .ToListAsync();

                var vm = new TaxProfileListViewModel {
                    TaxProfiles = _mapper.Map<List<TaxProfile>, List<TaxProfileDetailViewModel>>(entityList),
                    Count = entityList.Count
                };

                return vm;
            }
        }
    }
}