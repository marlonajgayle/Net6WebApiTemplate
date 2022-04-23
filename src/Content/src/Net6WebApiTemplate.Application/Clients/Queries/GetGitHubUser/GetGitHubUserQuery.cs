using MediatR;

namespace Net6WebApiTemplate.Application.Clients.Queries.GetGitHubUser
{
    public class GetGitHubUserQuery : IRequest<string>
    {
        public string Username { get; set; } = null!;
    }
}