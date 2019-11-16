using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.SystemUsers.Queries.Login
{
    public class LoginSystemUserQuery : IRequest<LoginSystemUserQueryResponse>
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public class LoginSystemUserQueryHandler : IRequestHandler<LoginSystemUserQuery, LoginSystemUserQueryResponse> {
            private readonly IBugLogDbContext _context;
            private readonly IPasswordGenerationService _passwordService;
            private readonly IJwtGeneratorService _jwtService;
            public LoginSystemUserQueryHandler(IBugLogDbContext context, IPasswordGenerationService passwordService, IJwtGeneratorService jwtService) {
                _context = context;
                _passwordService = passwordService;
                _jwtService = jwtService;
            }

            public async Task<LoginSystemUserQueryResponse> Handle(LoginSystemUserQuery request, CancellationToken cancellationToken) {
                var entity = await _context.SystemUsers.SingleOrDefaultAsync(x => x.EmailAddress == request.EmailAddress.ToLower());
                if(entity == null) {
                    throw new EntityNotFoundException(nameof(SystemUser), request.EmailAddress);
                }
                if(!entity.IsActive) {
                    throw new AuthException(nameof(SystemUser), "User account is deactivated. Please contact the system administrator.");
                }
                
                if(!entity.IsLocked) {
                    throw new AuthException(nameof(SystemUser), "User account is locked. Please contact the system administrator.");
                }

                if(!entity.IsVerified) {
                    throw new AuthException(nameof(SystemUser), "User account is not verified. Please verify your user account or contact the system administrator.");
                }

                if(!_passwordService.VerifyPasswordHash(request.Password, entity.PasswordHash, entity.PasswordSalt)) {
                    throw new BadRequestException("Email address or password is incorrect.");
                }

                var vm = new LoginSystemUserQueryResponse(_jwtService.GenerateJwtToken(entity));
                return vm;
            }
        }
    }
}