using System;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.SystemUsers.Commands.Register
{
    public class RegisterSystemUserCommand : IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public Guid? UserManagerId { get; set; }
        public class RegisterSystemUserCommandHandler : IRequestHandler<RegisterSystemUserCommand, Guid> {
            
            private readonly IBugLogDbContext _context;
            private readonly IPasswordGenerationService _passwordService;
            public RegisterSystemUserCommandHandler(IBugLogDbContext context, IPasswordGenerationService passwordService) {
                _context = context;
                _passwordService = passwordService;
            }
            public async Task<Guid> Handle(RegisterSystemUserCommand request, CancellationToken cancellationToken) {
                var userExists = await _context.SystemUsers.Where(x => x.EmailAddress == request.EmailAddress.ToLower()).AnyAsync();
                if(userExists) {
                    throw new DuplicateUserException(nameof(SystemUser), request.EmailAddress);
                }

                if(request.UserManagerId.HasValue) {
                    var managerExists = await _context.SystemUsers.AnyAsync(x => x.Id == request.UserManagerId.Value);
                    if(!managerExists) {
                        throw new EntityNotFoundException(nameof(SystemUser), request.UserManagerId.Value);
                    }
                }

                _passwordService.GeneratePassword(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var entity = new SystemUser {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    EmailAddress = request.EmailAddress.ToLower(),
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    UserManagerId = request.UserManagerId
                };

                await _context.SystemUsers.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}