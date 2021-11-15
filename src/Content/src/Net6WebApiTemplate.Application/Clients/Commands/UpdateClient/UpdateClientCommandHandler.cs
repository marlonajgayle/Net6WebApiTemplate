using MediatR;
using Net6WebApiTemplate.Application.Common.Exceptions;
using Net6WebApiTemplate.Application.Common.Interfaces;
using Net6WebApiTemplate.Domain.Entities;

namespace Net6WebApiTemplate.Application.Clients.Commands.UpdateClient
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
    {
        private readonly INet6WebApiTemplateDbContext _dbContext;

        public UpdateClientCommandHandler(INet6WebApiTemplateDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _dbContext.Clients.FindAsync(request.Id, cancellationToken);

            if (client == null)
            {
                throw new NotFoundException(nameof(Client), request.Id);
            }

            // Patch update
            client.FirstName = request.FirstName ?? client.FirstName;
            client.LastName = request.LastName ?? client.LastName;

            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}