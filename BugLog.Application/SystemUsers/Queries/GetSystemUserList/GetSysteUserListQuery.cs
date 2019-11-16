using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using BugLog.Application.Interfaces;
using BugLog.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.SystemUsers.Queries
{
    public class GetSystemUserListQuery : IRequest<SystemUserListViewModel>
    {
        public class GetSystemUserListQueryHandler : IRequestHandler<GetSystemUserListQuery, SystemUserListViewModel> {
            private readonly IBugLogDbContext _context;
            private readonly IMapper _mapper;

            public GetSystemUserListQueryHandler(IBugLogDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }
            public async Task<SystemUserListViewModel> Handle(GetSystemUserListQuery request, CancellationToken cancellationToken) {
                var entityList = await _context.SystemUsers.ToListAsync();
                var vm = new SystemUserListViewModel {
                    SystemUsers = _mapper.Map<List<SystemUser>, List<SystemUserDetailViewModel>>(entityList),
                    Count = entityList.Count
                };
                return vm;
            }
        }
    }
}