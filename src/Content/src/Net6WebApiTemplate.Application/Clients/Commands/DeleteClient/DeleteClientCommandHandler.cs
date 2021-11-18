using MediatR;
using Net6WebApiTemplate.Application.Common.Exceptions;
using Net6WebApiTemplate.Application.Common.Interfaces;

namespace Net6WebApiTemplate.Application.Clients.Commands.DeleteClient
{
    public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand>
    {
        private readonly INet6WebApiTemplateDbContext _dbContext;

        public DeleteClientCommandHandler(INet6WebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FindAsync(request.Id, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(client), request.Id);
            }

            _dbContext.Clients.Remove(client);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}