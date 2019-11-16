using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Interfaces;
using BugLog.Domain.Entities;

namespace BugLog.Application.Contacts.Commands
{
    public class CreateContactCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string MobilePhone { get; set; }
        public Guid CustomerId { get; set; }

        public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid> {
            private readonly IBugLogDbContext _context;

            public CreateContactCommandHandler(IBugLogDbContext context)
            {
                _context = context;
            }

            public async Task<Guid> Handle(CreateContactCommand request, CancellationToken cancellationToken) {
                var entity = new Contact {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MobilePhone = request.MobilePhone,
                    EmailAddress = request.EmailAddress,
                    CustomerId = request.CustomerId
                };

                await _context.Contacts.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}