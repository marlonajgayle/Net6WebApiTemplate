using MediatR;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
{
    public Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
