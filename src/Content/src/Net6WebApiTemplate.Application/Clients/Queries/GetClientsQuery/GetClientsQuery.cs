using MediatR;

namespace Net6WebApiTemplate.Application.Clients.Commands.Queries.GetClientsQuery
{
    public class GetClientsQuery : IRequest<IList<ClientDto>>
    {
    }
}