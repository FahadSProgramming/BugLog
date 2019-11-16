using MediatR;
using BugLog.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BugLog.Domain.Entities;

namespace BugLog.Application.Countries.Queries
{
    public class GetCountryListQuery : IRequest<CountryListViewModel>
    {

        public class GetCountryListQueryHandler : IRequestHandler<GetCountryListQuery, CountryListViewModel> {
            private readonly IBugLogDbContext _context;
            private readonly IMapper _mapper;

            public GetCountryListQueryHandler(IBugLogDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CountryListViewModel> Handle(GetCountryListQuery request, CancellationToken cancellationToken) {
                var entityList = await _context.Countries
                .Include(x => x.Customers)
                .Include(x => x.Currency)
                .Include(x => x.Contacts)
                .ToListAsync(cancellationToken);

                var vm = new CountryListViewModel {
                    Countries = _mapper.Map<List<Country>, List<CountryDetailViewModel>>(entityList),
                    Count = entityList.Count
                };
                
                return vm;
            }
        }
    }
}