using MediatR;
using Microsoft.EntityFrameworkCore;
using Net6WebApiTemplate.Application.Common.Exceptions;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Application.Products.Dto;
using Net6WebApiTemplate.Domain.Entities;
namespace Net6WebApiTemplate.Application.Products.Commands.PatchProduct;

public class PatchProductCommandHandler : IRequestHandler<PatchProductCommand, ProductDto>
{
    private readonly INet6WebApiTemplateDbContext _dbContext;

    public PatchProductCommandHandler(INet6WebApiTemplateDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ProductDto> Handle(PatchProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await Task.Run(() => _dbContext
              .Products
              .Include(s => s.Category)
              .Where(s => s.Id.Equals(request.Id))
              .FirstOrDefault(), cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Client), request.Id);
        }

        // Patch update
        product.ProductName = request.ProductName ?? product.ProductName;
        product.CategoryId = request.CategoryId ?? product.CategoryId;
        product.UnitPrice = request.UnitPrice ?? product.UnitPrice;

        await _dbContext.SaveChangesAsync(cancellationToken);

        ProductDto productDto = new()
        {
            CategoryId = product.CategoryId,
            UnitPrice = product.UnitPrice,
            ProductName = request.ProductName
        };

        return productDto;
    }
}
