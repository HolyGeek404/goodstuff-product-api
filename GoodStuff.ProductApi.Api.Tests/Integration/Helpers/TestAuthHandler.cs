using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GoodStuff.ProductApi.Api.Tests.Integration.Helpers;
public class TestAuthHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Create a fake identity with any roles you want
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "TestProduct"),
            new Claim(ClaimTypes.Role, "Get"),
            new Claim(ClaimTypes.Role, "Update"),
            new Claim(ClaimTypes.Role, "Create"),
            new Claim(ClaimTypes.Role, "Delete")
        };

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
