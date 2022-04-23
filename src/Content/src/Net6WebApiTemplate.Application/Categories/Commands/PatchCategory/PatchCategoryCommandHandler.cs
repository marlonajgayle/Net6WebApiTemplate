using MediatR;
using Net6WebApiTemplate.Application.Categories.Commands.PatchCategory;
using Net6WebApiTemplate.Application.Categories.Dto;
using Net6WebApiTemplate.Application.Common.Exceptions;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Domain.Entities;
namespace Net6WebApiTemplate.Application.Categories.Commands.PatchProduct;

public class PatchCategoryCommandHandler : IRequestHandler<PatchCategoryCommand, CategoryDto>
{
    private readonly INet6WebApiTemplateDbContext _dbContext;

    public PatchCategoryCommandHandler(INet6WebApiTemplateDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<CategoryDto> Handle(PatchCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FindAsync(request.Id, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(Client), request.Id);
        }

        // Patch update
        category.CategoryName = request.CategoryName ?? category.CategoryName;
        category.Description = request.Description ?? category.Description;

        await _dbContext.SaveChangesAsync(cancellationToken);

        CategoryDto categoryDto = new()
        {
            CategoryName = category.CategoryName,
            Description = category.Description
        };

        return categoryDto;
    }
}
