using MediatR;
using Net6WebApiTemplate.Application.Common.Exceptions;
using Net6WebApiTemplate.Application.Common.Interfaces;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IMediator _mediator;
    private readonly INet6WebApiTemplateDbContext _dbContext;

    public DeleteProductCommandHandler(IMediator mediator, INet6WebApiTemplateDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products.FindAsync(request.Id, cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(product), request.Id);
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

}
