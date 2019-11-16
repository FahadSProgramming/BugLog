using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Interfaces;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;

namespace BugLog.Application.Contacts.Commands
{
    public class UpdateContactCommand : IRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public bool IsActive { get; set; }
        public Guid CustomerId { get; set; }

        public class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand, Unit>
        {
            private readonly IBugLogDbContext _context;
            public UpdateContactCommandHandler(IBugLogDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateContactCommand request, CancellationToken cancellationToken) {
                var entity = await _context.Contacts.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Contact), request.Id);
                }
                entity.FirstName = request.FirstName;
                entity.LastName = request.LastName;
                entity.EmailAddress = request.EmailAddress;
                entity.MobilePhone = request.MobilePhone;
                entity.CustomerId = request.CustomerId;
                entity.IsActive = request.IsActive;

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}