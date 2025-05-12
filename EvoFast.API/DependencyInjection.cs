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
                options.Authority = "https://evofast-identityserver.solocode.click";
                options.Audience = "EvoFastAPI";
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "https://evofast-identityserver.solocode.click",
                    ValidAudience = "EvoFastAPI"
                };
            });
            
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ClientIdPolicy", policy => policy.RequireClaim("client_id", "m2m.client"));
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