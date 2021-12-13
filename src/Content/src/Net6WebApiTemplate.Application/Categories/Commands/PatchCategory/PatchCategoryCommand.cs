using MediatR;
using Net6WebApiTemplate.Application.Categories.Dto;

namespace Net6WebApiTemplate.Application.Categories.Commands.PatchCategory;
public class PatchCategoryCommand : IRequest<CategoryDto>
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }

}
