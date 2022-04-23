using MediatR;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Application.Products.Dto;
using Net6WebApiTemplate.Domain.Entities;
namespace Net6WebApiTemplate.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IMediator _mediator;
    private readonly INet6WebApiTemplateDbContext _dbContext;

    public CreateProductCommandHandler(IMediator mediator, INet6WebApiTemplateDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new()
        {
            CategoryId = request.CategoryId,
            ProductName = request.ProductName,
            UnitPrice = request.UnitPrice
        };

        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        ProductDto productDto = new()
        {
            CategoryId = request.CategoryId,
            ProductName = request.ProductName,
            UnitPrice = request.UnitPrice
        };
        return productDto;
    }
}
