using BuildingBlocks.Exceptions.Handler;
using FluentValidation;

namespace EvoFast.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddExceptionHandler<CustomExceptionHandler>();
        return services;
    }
    
    public static WebApplication UseApiServices(this WebApplication app)
    {

        app.UseExceptionHandler(options => { });
        return app;
    }
}