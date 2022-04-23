using MediatR;
namespace Net6WebApiTemplate.Application.Products.Commands.DeleteProduct;
public class DeleteProductCommand : IRequest<bool>
{
    public int Id { get; set; }
}
