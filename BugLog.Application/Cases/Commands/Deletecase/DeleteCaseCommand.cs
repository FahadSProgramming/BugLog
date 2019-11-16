using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;

namespace BugLog.Application.Cases.Commands
{
    public class DeleteCaseCommand : IRequest
    {
        public Guid Id { get; set; }

        public class DeleteCaseCommandHandler : IRequestHandler<DeleteCaseCommand, Unit> {
            private readonly IBugLogDbContext _context;
            public DeleteCaseCommandHandler(IBugLogDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(DeleteCaseCommand request, CancellationToken cancellationToken) {
                
                var entity = await _context.Cases.FindAsync(request.Id);

                if(entity == null) {
                    throw new Exception("Case with Id does not exixt.");
                    // throw custom notfound exception;
                } else {
                    _context.Cases.Remove(entity);
                    await _context.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
                
            }
        }
    }
}