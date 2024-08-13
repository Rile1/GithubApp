using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MicrosoftDataApi.Models;
using MicrosoftDataApi.Services;
using Newtonsoft.Json;

namespace MicrosoftDataApi.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IGitHubService _gitHubService;

    public AccountController(IGitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }

    [HttpGet("login-with-github")]
    public IActionResult LoginWithGitHub()
    {
        var redirectUrl = Url.Action("GitHubLoginCallback", "Account");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, "GitHub");
    }

    [HttpGet("signin-github")]
    public async Task<IActionResult> GitHubLoginCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync();

        if (!authenticateResult.Succeeded)
        {
            return Unauthorized("You are not authorized to access this resource.");
        }

        var accessToken = authenticateResult.Properties.GetTokenValue("access_token");
        HttpContext.Session.SetString("GitHubAccessToken", accessToken);

        return Redirect("http://localhost:4200/repos");
    }

    [HttpGet("repositories")]
    public async Task<IActionResult> GetRepositories()
    {
        var accessToken = HttpContext.Session.GetString("GitHubAccessToken");

        if (string.IsNullOrEmpty(accessToken))
        {
            return Unauthorized("You are not authorized to access this resource.");
        }

        var repositories = await _gitHubService.FetchGitHubRepositories(accessToken);

        return Ok(repositories);
    }

    [HttpPost("save")]
    public IActionResult SaveCollection([FromBody] List<GitHubRepository> repositories)
    {
        string json = JsonConvert.SerializeObject(repositories);
        System.IO.File.WriteAllText("Files/repositories.json", json);
        return Ok(true);
    }
}
