using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BugLog.Application.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;

namespace BugLog.Application.Contacts.Queries.GetContactDetail
{
    public class GetContactDetailQueryHandler : IRequestHandler<GetContactDetailQuery, ContactDetailViewModel>
    {
        private readonly IBugLogDbContext _context;
        private readonly IMapper _mapper;

        public GetContactDetailQueryHandler(IBugLogDbContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ContactDetailViewModel> Handle(GetContactDetailQuery request, CancellationToken cancellationToken) {
            var entity = await _context.Contacts
            .Include(c => c.Customer)
            .SingleOrDefaultAsync(x => x.Id == request.Id);

            if(entity == null)
             {
                 throw new EntityNotFoundException(nameof(Contact), request.Id);
             }

             var vm = _mapper.Map<Contact, ContactDetailViewModel>(entity);

             return vm;
        }
    }
}