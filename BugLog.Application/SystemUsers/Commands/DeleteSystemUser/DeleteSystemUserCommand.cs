using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using BugLog.Domain.Entities;

namespace BugLog.Application.SystemUsers.Commands
{
    public class DeleteSystemUserCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteSystemUserCommandHandler : IRequestHandler<DeleteSystemUserCommand> {
            private readonly IBugLogDbContext _context;

            public DeleteSystemUserCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteSystemUserCommand request, CancellationToken cancellationToken) {
                var entity = await _context.SystemUsers.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(SystemUser), request.Id);
                }

                _context.SystemUsers.Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}