using BuildingBlocks.Exceptions.Handler;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;

namespace EvoFast.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddHealthChecks().AddNpgSql(configuration.GetConnectionString("Database")!);
        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://meidox-identityserver.solocode.click/realms/api-gateway";
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidAudiences = ["account"],
                    ValidIssuers = ["https://meidox-identityserver.solocode.click/realms/api-gateway"],
                    ClockSkew = TimeSpan.Zero
                };
            });
        return services;
    }
    
    public static WebApplication UseApiServices(this WebApplication app)
    {

        app.UseExceptionHandler(options => { });
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        return app;
    }
    
    public static void AddCors(this IServiceCollection services, string? originString)
    {
        var origins = originString?.Split(";");

        services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
        {
            if (origins != null)
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
                    .WithOrigins(origins);
        }));
    }
}