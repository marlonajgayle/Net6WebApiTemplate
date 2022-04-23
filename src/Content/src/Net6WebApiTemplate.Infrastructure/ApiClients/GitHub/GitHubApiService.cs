using Net6WebApiTemplate.Application.Common.Interfaces;

namespace Net6WebApiTemplate.Infrastructure.ApiClients.GitHub
{
    public class GitHubApiService : IGitHubService
    {
        private readonly HttpClient _httpClient;

        public GitHubApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(name:"GitHub");
        }

        public async Task<string> LoadAccountAsync(string username)
        {
            var response = await _httpClient.GetStringAsync($"/users/{username}");
            return response;
        }
    }
}