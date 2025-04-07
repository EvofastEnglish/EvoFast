using EvoFast.Infrastructure.Data;
using EvoFast.Tests.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EvoFast.Tests;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    private IConfiguration? _testConfiguration;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            configBuilder.AddJsonFile("appsettings.Test.json");
            _testConfiguration = configBuilder.Build();
        });
        
        builder.ConfigureServices(services =>
        {
            // Clear existing DbContextOptions if any
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            if (descriptor != null)
                services.Remove(descriptor);

            // Replace authentication for testing
            services.AddTestAuthentication();

            // Prepare the database
            services.PrepareTestDatabase(_testConfiguration);
            
            services.AddDependencyInjection();
        });
    }
}