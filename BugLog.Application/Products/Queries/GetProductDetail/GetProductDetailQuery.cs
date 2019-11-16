using System;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Products.Queries
{
    public class GetProductDetailQuery : IRequest<ProductDetailViewModel>
    {
        public Guid Id { get; set; }

        public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProductDetailViewModel> {
            private readonly IBugLogDbContext _context;
            private readonly IMapper _mapper;

            public GetProductDetailQueryHandler(IBugLogDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ProductDetailViewModel> Handle(GetProductDetailQuery request, CancellationToken cancellationToken) {
                var entity = await _context.Products
                            .Include(x => x.DefaultPriceList)
                            .SingleOrDefaultAsync(x => x.Id == request.Id);

                if(entity == null) {
                    throw new EntityNotFoundException(nameof(Product), request.Id);
                }

                var vm = _mapper.Map<Product, ProductDetailViewModel>(entity);
                return vm;
            }
 
        }
    }
}