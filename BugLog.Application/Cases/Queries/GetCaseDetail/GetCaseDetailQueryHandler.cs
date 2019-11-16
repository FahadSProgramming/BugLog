using AutoMapper;
using MediatR;
using System;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Exceptions;

namespace BugLog.Application.Cases.Queries
{
    public class GetCaseDetailQueryHandler : IRequestHandler<GetCaseDetailQuery, CaseDetailViewModel> {
        private readonly IBugLogDbContext _context;
        private readonly IMapper _mapper;

        public GetCaseDetailQueryHandler(IBugLogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CaseDetailViewModel> Handle(GetCaseDetailQuery request, CancellationToken cancellationToken) {
            var entity = await _context.Cases.FindAsync(request.Id);

            if(entity == null) {
                throw new EntityNotFoundException(nameof(Case), request.Id);
            }
            var vm = _mapper.Map<Case, CaseDetailViewModel>(entity);
            return vm;
        }
    }
}