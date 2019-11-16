using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.SystemUsers.Commands
{
    public class UpdateUserCommand : IRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLocked { get; set; }
        public Guid? UserManagerId { get; set; }

        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit> {
            private readonly IBugLogDbContext _context;
            public UpdateUserCommandHandler(IBugLogDbContext context) {
                _context = context;
            }
            public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken) {
                var entity = await _context.SystemUsers.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(SystemUser), request.Id);
                }

                if(request.UserManagerId.HasValue) {
                    var userManager = await _context.SystemUsers.AnyAsync(x => x.Id == request.UserManagerId.Value);

                    if(!userManager) {
                        throw new BadRequestException("Referenced user manager does not exist. The operation cannot be completed.");
                    }

                    entity.UserManagerId = request.UserManagerId.Value;
                }

                if(!string.IsNullOrEmpty(request.FirstName)) {
                    entity.FirstName = request.FirstName;
                }
                if(!string.IsNullOrEmpty(request.LastName)) {
                    entity.LastName = request.LastName;
                }

                if(!string.IsNullOrEmpty(request.EmailAddress) && !entity.EmailAddress.Equals(request.EmailAddress)) {
                    entity.EmailAddress = request.EmailAddress;
                }

                if(request.IsVerified.HasValue) {
                    entity.IsVerified = request.IsVerified.Value;
                }

                if(request.IsActive.HasValue) {
                    entity.IsActive = request.IsActive.Value;
                }

                if(request.IsLocked.HasValue) {
                    entity.IsLocked = request.IsLocked.Value;
                }

                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;

            }
        }
    }
}