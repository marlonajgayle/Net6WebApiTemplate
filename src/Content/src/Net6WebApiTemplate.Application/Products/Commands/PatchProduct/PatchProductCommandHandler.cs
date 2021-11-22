using MediatR;
using Net6WebApiTemplate.Domain.Entities;
using System;

public class PatchProductCommandHandler : IRequestHandler<PatchProductCommand, Product>
{
    public Task<Product> Handle(PatchProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
 