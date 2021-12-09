using MediatR;
using Net6WebApiTemplate.Application.Common.Exceptions;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Domain.Entities;
using System;

public class PatchProductCommandHandler : IRequestHandler<PatchProductCommand, Product>
{
    private readonly INet6WebApiTemplateDbContext _dbContext;

    public PatchProductCommandHandler(INet6WebApiTemplateDbContext dbContext)
    {
        _dbContext=dbContext;
    }
    public async Task<Product> Handle(PatchProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FindAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(Client), request.Id);
        }

        // Patch update
        product.ProductName = request.ProductName ?? product.ProductName;
        product.CategoryId = request.CategoryId ?? product.CategoryId;
        product.UnitPrice = request.UnitPrice ?? product.UnitPrice;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return product;
    }
}
 