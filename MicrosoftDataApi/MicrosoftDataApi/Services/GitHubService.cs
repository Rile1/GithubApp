using System.Net.Http.Headers;
using MicrosoftDataApi.Models;
using Newtonsoft.Json;

namespace MicrosoftDataApi.Services;

public class GitHubService: IGitHubService
{
    private readonly HttpClient _client;

    public GitHubService(HttpClient client)
    {
        _client = client;
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("MicrosoftDataApi", "1.0"));
    }

    public async Task<List<GitHubRepository>> FetchGitHubRepositories(string accessToken)
    {
        try
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await _client.GetAsync("https://api.github.com/user/repos");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"GitHub API request failed with status code {response.StatusCode}: {response.ReasonPhrase}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var repositories = JsonConvert.DeserializeObject<List<GitHubRepository>>(jsonResponse);

            return repositories;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("An error occurred while fetching GitHub repositories.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred while processing your request.", ex);
        }
    }
}
