using System.Reflection;
using System.Text.Json;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using EvoFast.Application.Dtos;
using EvoFast.Application.Mapper;
using EvoFast.Domain.Models;
using FluentValidation;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EvoFast.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        MapsterConfig.Configure();
        services.Configure<Dictionary<string, string>>(options =>
        {
            var path = Path.Combine(AppContext.BaseDirectory, "Resources", "languages.json");
            var json = File.ReadAllText(path);
            var languages = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            foreach (var language in languages)
            {
                options[language.Key] = language.Value;
            }
        });
        return services;
    }
}