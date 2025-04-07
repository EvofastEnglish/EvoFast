using EvoFast.Infrastructure.Data;
using EvoFast.Tests.Extensions.Provider;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvoFast.Tests.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddTestAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication("DefineForTest")
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("DefineForTest", options => { });

        services.Configure<AuthenticationOptions>(options =>
        {
            options.DefaultAuthenticateScheme = "DefineForTest";
            options.DefaultChallengeScheme = "DefineForTest";
        });
    }

    public static void PrepareTestDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration?.GetConnectionString("Database")
                               ?? throw new Exception("Connection string not found");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        
        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.EnsureDeleted();
        db.Database.Migrate();
    }
    
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddSingleton<ITestUserProvider, TestUserProvider>();
    }
}
