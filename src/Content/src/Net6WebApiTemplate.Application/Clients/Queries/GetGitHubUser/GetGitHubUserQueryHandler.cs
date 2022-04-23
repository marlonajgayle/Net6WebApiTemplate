using MediatR;
using Net6WebApiTemplate.Application.Common.Interfaces;

namespace Net6WebApiTemplate.Application.Clients.Queries.GetGitHubUser
{
    public class GetGitHubUserQueryHandler : IRequestHandler<GetGitHubUserQuery, string>
    {
        private readonly IGitHubService  _gitHubService;

        public GetGitHubUserQueryHandler(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<string> Handle(GetGitHubUserQuery request, CancellationToken cancellationToken)
        {
            var userInfo = await _gitHubService.LoadAccountAsync(request.Username);
            return userInfo;
        }
    }
}