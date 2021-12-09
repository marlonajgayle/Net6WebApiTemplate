using MediatR;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Domain.Entities;
using System;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand,Product>
{
    private readonly IMediator _mediator;
    private readonly INet6WebApiTemplateDbContext _dbContext;

    public CreateProductCommandHandler(IMediator mediator, INet6WebApiTemplateDbContext dbContext)
	{
        _mediator = mediator;
        _dbContext = dbContext;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new()
        {
            Category = request.Category,
            CategoryId = request.CategoryId,
            ProductName = request.ProductName,
            UnitPrice = request.UnitPrice
        };

         _dbContext.Products.Add(product);
         await _dbContext.SaveChangesAsync(cancellationToken);

        return product;
    }
}
