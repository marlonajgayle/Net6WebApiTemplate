using MediatR;
using Net6WebApiTemplate.Application.Products.Dto;
namespace Net6WebApiTemplate.Application.Products.Commands.PatchProduct;
public class PatchProductCommand : IRequest<ProductDto>
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal? UnitPrice { get; set; }
    public int? CategoryId { get; set; }
}
