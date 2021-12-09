using MediatR;
using Microsoft.EntityFrameworkCore;
using Net6WebApiTemplate.Application.Common.Exceptions;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Application.Products.NQueries.GetProducts;
using Net6WebApiTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6WebApiTemplate.Application.Products.NQueries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly INet6WebApiTemplateDbContext _dbContext;

        public GetProductByIdQueryHandler(INet6WebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product result = await Task.Run(() => _dbContext
               .Products
               .Include(s => s.Category)
               .Where(s => s.Id.Equals(request.Id))
               .FirstOrDefault(), cancellationToken);


            if (result == null)
            {
                throw new NotFoundException($"Product with id {request.Id} not found.");
            }

            ProductDto product = new()
            {
                Id = request.Id,
                Category = result.Category,
                ProductName = result.ProductName,
                CategoryId = result.CategoryId,
                UnitPrice = result.UnitPrice
            };

            return product;
        }
    }
}
