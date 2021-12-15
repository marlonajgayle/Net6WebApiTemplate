using MediatR;
namespace Net6WebApiTemplate.Application.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommand : IRequest<bool>
{
    public int Id { get; set; }
}
