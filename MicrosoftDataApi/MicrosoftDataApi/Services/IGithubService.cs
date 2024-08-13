using MicrosoftDataApi.Models;

namespace MicrosoftDataApi.Services
{
    public interface IGitHubService
    {
        Task<List<GitHubRepository>> FetchGitHubRepositories(string accessToken);
    }
}
