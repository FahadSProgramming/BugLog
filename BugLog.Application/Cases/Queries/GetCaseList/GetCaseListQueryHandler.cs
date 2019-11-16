using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Interfaces;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using BugLog.Domain.Entities;

namespace BugLog.Application.Cases.Queries
{
    public class GetCaseListQueryHandler : IRequestHandler<GetCaseListQuery, CaseListViewModel> {
        private readonly IBugLogDbContext _context;
        private readonly IMapper _mapper;
        public GetCaseListQueryHandler(IBugLogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CaseListViewModel> Handle(GetCaseListQuery request, CancellationToken cancellationToken) {
            //var case = _mapper.Map<CaseDetailViewModel>(await _context.Cases.ToListAsync());

            var entityList = await _context.Cases
            //.ProjectTo<CaseDetailViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

            var vm = new CaseListViewModel {
                Cases = _mapper.Map<List<Case>, List<CaseDetailViewModel>>(entityList),
                Count = entityList.Count
            };

            return vm;
        }
    }
}