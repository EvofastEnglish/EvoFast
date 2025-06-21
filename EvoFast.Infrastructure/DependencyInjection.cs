using EvoFast.Application.Data;
using EvoFast.Application.Services;
using EvoFast.Infrastructure.Interceptors;
using EvoFast.Infrastructure.Services;
using EvoFast.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Audio;

namespace EvoFast.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices
        (this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        // Bind config section
        var openAiSettings = new OpenAiSettings();
        configuration.GetSection(nameof(OpenAiSettings)).Bind(openAiSettings);
        services.AddSingleton(openAiSettings);
        
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddScoped<IWhisperService, WhisperService>();
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddHttpContextAccessor();
        services.AddHttpClient<IChatGptService, ChatGptService>();
        services.AddChatClient(_ =>
            new OpenAI.Chat.ChatClient(openAiSettings.Model, openAiSettings.ApiKey)
                .AsIChatClient());
        return services;
    }
}