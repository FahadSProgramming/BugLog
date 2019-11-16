using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Application.Interfaces;
using BugLog.Domain.Entities;

namespace BugLog.Application.Countries.Commands
{
    public class CreateCountryCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string TwoDigitISOCode { get; set; }
        public class CreateCountryCommandHandler: IRequestHandler<CreateCountryCommand, Guid> {
            private readonly IBugLogDbContext _context;

            public CreateCountryCommandHandler(IBugLogDbContext context) {
                _context = context;
            }

            public async Task<Guid> Handle(CreateCountryCommand request, CancellationToken cancellationToken) {
                var entity = new Country {
                    Name = request.Name,
                    TwoDigitISOCode = request.TwoDigitISOCode
                };
                await _context.Countries.AddAsync(entity);
                await _context.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}