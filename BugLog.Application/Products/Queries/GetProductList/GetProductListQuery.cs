using System;
using MediatR;
using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using BugLog.Domain.Entities;
using System.Collections.Generic;
using BugLog.Application.Interfaces;
using BugLog.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BugLog.Application.Products.Queries
{
    public class GetProductListQuery : IRequest<ProductListViewModel>
    {
        public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, ProductListViewModel> {
            private readonly IBugLogDbContext _context;
            private readonly IMapper _mapper;

            public GetProductListQueryHandler(IBugLogDbContext context, IMapper mapper) {
                _context = context;
                _mapper = mapper;
            }

            public async Task<ProductListViewModel> Handle(GetProductListQuery request, CancellationToken cancellationToken) {
                var entityList = await _context.Products
                .Include(x => x.DefaultPriceList)
                .ToListAsync();

                var vm = new ProductListViewModel {
                    Products = _mapper.Map<List<Product>, List<ProductDetailViewModel>>(entityList),
                    Count = entityList.Count
                };

                return vm;
            }
        }
    }
}