using MediatR;
using Microsoft.EntityFrameworkCore;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Application.Products.Dto;

namespace Net6WebApiTemplate.Application.Products.NQueries.GetProducts
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IList<ProductDto>>
    {
        private readonly INet6WebApiTemplateDbContext _dbContext;

        public GetProductsQueryHandler(INet6WebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _dbContext.Products
               .Include(s => s.Category)
               .Select(product => new ProductDto
               {
                   Id = product.Id,
                   ProductName = product.ProductName,
                   UnitPrice = product.UnitPrice,
                   CategoryId = product.CategoryId,
                   Category = product.Category
               })
               .ToListAsync(cancellationToken);

            return products;

        }
    }
}
