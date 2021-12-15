using MediatR;
using Net6WebApiTemplate.Application.Products.Dto;
namespace Net6WebApiTemplate.Application.Products.Commands.CreateProduct;
public class CreateProductCommand : IRequest<ProductDto>
{
    public string? ProductName { get; set; }
    public decimal? UnitPrice { get; set; }
    public int CategoryId { get; set; }
}
