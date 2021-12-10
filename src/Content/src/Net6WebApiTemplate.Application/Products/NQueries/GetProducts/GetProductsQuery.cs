using MediatR;
using Net6WebApiTemplate.Application.Products.Dto;

namespace Net6WebApiTemplate.Application.Products.NQueries.GetProducts
{
    public class GetProductsQuery : IRequest<IList<ProductDto>>
    {

    }
}
