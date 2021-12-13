using MediatR;
using Net6WebApiTemplate.Application.Categories.Dto;

namespace Net6WebApiTemplate.Application.Categories.NQueries.GetCategory
{
    public class GetCategoryQuery : IRequest<IList<CategoryDto>>
    {

    }
}
