using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Interfaces;

namespace BugLog.Application.Contacts.Commands
{
    public class DeleteContactCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, Unit> {
            private readonly IBugLogDbContext _context;
            public DeleteContactCommandHandler(IBugLogDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteContactCommand request, CancellationToken cancellationToken) {
                var entity = await _context.Contacts.FindAsync(request.Id);

                if(entity == null) {
                    // throw 404
                }
                _context.Contacts.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}