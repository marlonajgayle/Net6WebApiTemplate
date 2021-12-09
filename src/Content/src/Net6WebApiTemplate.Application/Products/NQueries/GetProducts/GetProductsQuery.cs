using MediatR;
using Net6WebApiTemplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6WebApiTemplate.Application.Products.NQueries.GetProducts
{
    public class GetProductsQuery : IRequest<IList<ProductDto>>
    {

    }
}
