using MediatR;
using Net6WebApiTemplate.Application.Categories.Dto;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Domain.Entities;
namespace Net6WebApiTemplate.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly IMediator _mediator;
    private readonly INet6WebApiTemplateDbContext _dbContext;

    public CreateCategoryCommandHandler(IMediator mediator, INet6WebApiTemplateDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category category = new()
        {
            Description = request.Description,
            CategoryName = request.CategoryName
        };

        _dbContext.Categories.Add(category);
        await _dbContext.SaveChangesAsync(cancellationToken);

        CategoryDto categoryDto = new()
        {
            Description = request.Description,
            CategoryName = request.CategoryName
        };

        return categoryDto;
    }
}
