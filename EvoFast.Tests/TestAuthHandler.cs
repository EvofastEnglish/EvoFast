using System.Security.Claims;
using System.Text.Encodings.Web;
using EvoFast.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EvoFast.Tests;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock) {}

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[]
        {
            new Claim("client_id", "m2m.client"),
            new Claim(ClaimTypes.NameIdentifier, "0195383a-00e8-7150-8b3b-5fbee6e68eb7")
        };

        var identity = new ClaimsIdentity(claims, "DefineForTest");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "DefineForTest");
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}