using MediatR;
using Microsoft.EntityFrameworkCore;
using Net6WebApiTemplate.Application.Common.Exceptions;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Domain.Entities;

namespace Net6WebApiTemplate.Application.Products.Commands.DeleteProduct;
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
        Product product = await Task.Run(() => _dbContext
              .Products
              .Where(s => s.Id.Equals(request.Id))
              .FirstOrDefault(), cancellationToken);

        if (product == null)
        {
            throw new NotFoundException(nameof(product), request.Id);
        }

        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

}
