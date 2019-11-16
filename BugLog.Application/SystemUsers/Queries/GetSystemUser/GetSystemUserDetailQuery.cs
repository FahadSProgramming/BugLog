using System;
using MediatR;
using AutoMapper;
using System.Threading;
using BugLog.Domain.Entities;
using System.Threading.Tasks;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Exceptions;

namespace BugLog.Application.SystemUsers.Queries
{
    public class GetSystemUserDetailQuery : IRequest<SystemUserDetailViewModel>
    {
        public Guid Id { get; set; }
        public bool IncludeTaxProfileCreates { get; set; }
        public bool IncludeTaxProfileUpdates { get; set; }
        public bool IncludeCountryCreates { get; set; }
        public bool IncludeCountryUpdates { get; set; }
        public bool IncludeCurrencyCreates { get; set; }
        public bool IncludeCurrencyUpdates { get; set; }
        public class GetSystemUserDetailQueryHandler : IRequestHandler<GetSystemUserDetailQuery, SystemUserDetailViewModel> {
            private readonly IMapper _mapper;
            private readonly IBugLogDbContext _context;

            public GetSystemUserDetailQueryHandler(IMapper mapper, IBugLogDbContext context) {
                _mapper = mapper;
                _context = context;
            }

            public async Task<SystemUserDetailViewModel> Handle(GetSystemUserDetailQuery request, CancellationToken cancellationToken) {
                var entity = await _context.SystemUsers
                            .Include(x => x.Subordinates)
                            .Include(x => x.CurrencyCreateActions)
                            .Include(x => x.CurrencyUpdateActions)
                            .SingleOrDefaultAsync(x => x.Id == request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(SystemUser), request.Id);
                }

                var vm = _mapper.Map<SystemUser, SystemUserDetailViewModel>(entity);
                return vm;
            }
        }
    }
}