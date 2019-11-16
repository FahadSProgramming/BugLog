using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Infrastructure;
using BugLog.Application.Interfaces;

namespace BugLog.Application.Cases.Commands
{
    public class CreateCaseCommand : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? ContactId { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ServiceContractId { get; set; }
        public CaseStatusEnum Status { get; set; }
        public CasePriorityEnum Priority { get; set; }

        public class CreateCaseCommandHandler : IRequestHandler<CreateCaseCommand, Guid> {
            private readonly IBugLogDbContext _context;
            private readonly ISLACalculationService _slaService;
            public CreateCaseCommandHandler(IBugLogDbContext context, ISLACalculationService slaService)
            {
                _context = context;
                _slaService = slaService;
            }
            public async Task<Guid> Handle(CreateCaseCommand request, CancellationToken cancellationToken) {
                // Case entity;
                // if(request.Id.HasValue) {
                //     entity = await _context.Cases.FindAsync(request.Id.Value, cancellationToken);
                // } else {
                //     entity = new Case();
                //     await _context.Cases.AddAsync(entity, cancellationToken);
                // }
                var entity = new Case {
                    Title = request.Title,
                    Description = request.Description,
                    Status = request.Status.GetHashCode(),
                    Priority = request.Priority.GetHashCode(),
                    ExpectedEndDate = _slaService.CalculateExpectedDate(request.Priority),
                    ActualEndDate = _slaService.CalculateActualEndDate(request.Status),
                    ContactId = request.ContactId,
                    CustomerId = request.CustomerId
                };
                
                await _context.Cases.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return entity.Id;
            }
        }
    }
}