using MediatR;
using Net6WebApiTemplate.Application.Categories.Dto;
namespace Net6WebApiTemplate.Application.Categories.Commands.CreateCategory;
public class CreateCategoryCommand : IRequest<CategoryDto>
{
    public string CategoryName { get; set; }
    public string Description { get; set; }
}
