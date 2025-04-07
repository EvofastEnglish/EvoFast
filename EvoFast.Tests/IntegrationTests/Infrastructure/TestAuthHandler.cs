using System.Security.Claims;
using System.Text.Encodings.Web;
using EvoFast.Tests.Extensions.Provider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EvoFast.Tests;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly ITestUserProvider _userProvider;

    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        ITestUserProvider userProvider)
        : base(options, logger, encoder, clock)
    {
        _userProvider = userProvider;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[]
        {
            new Claim("client_id", "m2m.client"),
            new Claim(ClaimTypes.NameIdentifier, _userProvider.UserId.ToString()),
        };

        var identity = new ClaimsIdentity(claims, "DefineForTest");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "DefineForTest");
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}