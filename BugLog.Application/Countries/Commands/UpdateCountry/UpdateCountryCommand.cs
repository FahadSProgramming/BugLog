using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using BugLog.Domain.Entities;

namespace BugLog.Application.Countries.Commands
{
    public class UpdateCountryCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TwoDigitISOCode { get; set; }
        public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, Unit> {
            private readonly IBugLogDbContext _context;

            public UpdateCountryCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Unit> Handle(UpdateCountryCommand request, CancellationToken cancellationToken) {
                var entity = await _context.Countries.FindAsync(request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Country), request.Id);
                }

                entity.Name = request.Name ?? entity.Name;
                entity.TwoDigitISOCode = request.TwoDigitISOCode ?? entity.TwoDigitISOCode;

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}