using System;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Exceptions;
using BugLog.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.PriceLists.Queries
{
    public class GetPriceListDetailQuery : IRequest<PriceListDetailViewModel>
    {
        public Guid Id { get; set; }

        public class GetPriceListDetailQueryHandler : IRequestHandler<GetPriceListDetailQuery, PriceListDetailViewModel> {
            private readonly IBugLogDbContext _context;
            private readonly IMapper _mapper;

            public GetPriceListDetailQueryHandler(IBugLogDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<PriceListDetailViewModel> Handle(GetPriceListDetailQuery request, CancellationToken cancellationToken) {
                var entity = await _context.PriceLists
                .Include(x => x.Products)
                .SingleOrDefaultAsync(x => x.Id == request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(PriceList), request.Id);
                }

                var vm = _mapper.Map<PriceList, PriceListDetailViewModel>(entity);
                return vm;
            }
            
        }
    }
}