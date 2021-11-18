using MediatR;

namespace Net6WebApiTemplate.Application.Clients.Commands.DeleteClient
{
    public class DeleteClientCommand : IRequest
    {
        public int Id { get; set; }
    }
}