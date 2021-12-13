using MediatR;
using Net6WebApiTemplate.Application.Categories.Dto;

namespace Net6WebApiTemplate.Application.Categories.NQueries.GetCategoryById
{
    public class GetCategoryByIdQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }
}
