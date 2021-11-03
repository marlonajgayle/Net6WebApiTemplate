using MediatR;
using Net6WebApiTemplate.Domain.Entities;

namespace Net6WebApiTemplate.Application.Clients.Commands.CreateClient
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand>
    {
        public CreateClientCommandHandler()
        {

        }

        public async Task<Unit> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var entity = new Client
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Address = new(request.AddressLine1, request.AddressLine2, request.Parish)
            };

            return Unit.Value;
        }
    }
}