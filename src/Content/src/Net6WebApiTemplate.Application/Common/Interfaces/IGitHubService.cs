namespace Net6WebApiTemplate.Application.Common.Interfaces
{
    public interface IGitHubService
    {
        Task<String> LoadAccountAsync(string username);
    }
}