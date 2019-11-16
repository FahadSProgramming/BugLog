using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using AutoMapper;
using BugLog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Countries.Queries
{
    public class GetCountryDetailQuery : IRequest<CountryDetailViewModel>
    {
        public Guid Id { get; set; }

        public class GetCountryDetailQueryHandler : IRequestHandler<GetCountryDetailQuery, CountryDetailViewModel> {
            private readonly IBugLogDbContext _context;
            private readonly IMapper _mapper;

            public GetCountryDetailQueryHandler(IBugLogDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CountryDetailViewModel> Handle(GetCountryDetailQuery request, CancellationToken cancellationToken) {
                var entity = await _context.Countries
                .Include(x => x.Currency)
                .Include(x => x.Customers)
                .Include(x => x.Contacts)
                .SingleOrDefaultAsync(x => x.Id == request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Country), request.Id);
                }

                var vm = _mapper.Map<Country, CountryDetailViewModel>(entity);

                return vm;
            }
        }
    }
}