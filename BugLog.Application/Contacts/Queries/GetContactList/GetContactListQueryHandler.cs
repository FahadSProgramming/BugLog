using System.Collections.Generic;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;

namespace BugLog.Application.Contacts.Queries.GetContactList
{
    public class GetContactListQueryHandler : IRequestHandler<GetContactListQuery, ContactListViewModel>
    {
        private readonly IBugLogDbContext _context;
        private readonly IMapper _mapper;

        public GetContactListQueryHandler(IBugLogDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ContactListViewModel> Handle(GetContactListQuery request, CancellationToken cancellationToken) {
            var entityList = await _context.Contacts.ToListAsync(cancellationToken);

            var vm = new ContactListViewModel {
                Contacts = _mapper.Map<List<Contact>, List<ContactDetailViewModel>>(entityList),
                Count = entityList.Count
            };

            return vm;
        }
    }
}