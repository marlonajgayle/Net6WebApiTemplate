using MediatR;
using Net6WebApiTemplate.Application.Common.Exceptions;
using Net6WebApiTemplate.Application.Common.Interfaces;
namespace Net6WebApiTemplate.Application.Categories.Commands.DeleteCategory;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly IMediator _mediator;
    private readonly INet6WebApiTemplateDbContext _dbContext;

    public DeleteCategoryCommandHandler(IMediator mediator, INet6WebApiTemplateDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }
    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FindAsync(request.Id, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(category), request.Id);
        }

        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

}
