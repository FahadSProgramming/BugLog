using System;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;

namespace BugLog.Application.TaxProfiles.Queries
{
    public class GetTaxProfileDetailQuery : IRequest<TaxProfileDetailViewModel>
    {
        public Guid Id { get; set; }
        public class GetTaxProfileDetailQueryHandler : IRequestHandler<GetTaxProfileDetailQuery, TaxProfileDetailViewModel> {
            private readonly IBugLogDbContext _context;
            private readonly IMapper _mapper;
            public GetTaxProfileDetailQueryHandler(IBugLogDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }
            public async Task<TaxProfileDetailViewModel> Handle(GetTaxProfileDetailQuery request, CancellationToken cancellationToken) {
                var entity = await _context.TaxProfiles.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(TaxProfile), request.Id);
                }

                var vm = _mapper.Map<TaxProfile, TaxProfileDetailViewModel>(entity);
                return vm;

            }
        }
    }
}