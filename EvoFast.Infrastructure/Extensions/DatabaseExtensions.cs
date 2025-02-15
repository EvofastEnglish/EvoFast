using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EvoFast.Infrastructure.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.MigrateAsync().GetAwaiter().GetResult();
        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedWordSetAsync(context);
    }

    private static async Task SeedWordSetAsync(ApplicationDbContext context)
    {
        if (!await context.WordSets.AnyAsync())
        {
            await context.WordSets.AddRangeAsync(InitialData.WordSets);
            await context.SaveChangesAsync();
        }
    }
}
