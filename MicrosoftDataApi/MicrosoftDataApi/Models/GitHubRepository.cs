using Newtonsoft.Json;

namespace MicrosoftDataApi.Models;

public class GitHubRepository
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("html_url")]
    public string HtmlUrl { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }
}
