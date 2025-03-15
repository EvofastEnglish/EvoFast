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
        await SeedQuestionAsync(context);
        await SeedAnswersAsync(context);
        await SeedCategoriesAsync(context);
    }

    private static async Task SeedWordSetAsync(ApplicationDbContext context)
    {
        if (!await context.WordSets.AnyAsync())
        {
            await context.WordSets.AddRangeAsync(InitialData.WordSets);
            await context.SaveChangesAsync();
        }
    }
    
    private static async Task SeedQuestionAsync(ApplicationDbContext context)
    {
        if (!await context.Questions.AnyAsync())
        {
            await context.Questions.AddRangeAsync(InitialData.Questions);
            await context.SaveChangesAsync();
        }
    }
    
    private static async Task SeedAnswersAsync(ApplicationDbContext context)
    {
        if (!await context.Answers.AnyAsync())
        {
            await context.Answers.AddRangeAsync(InitialData.Answers);
            await context.SaveChangesAsync();
        }
    }
    
    private static async Task SeedCategoriesAsync(ApplicationDbContext context)
    {
        if (!await context.Categories.AnyAsync())
        {
            await context.Categories.AddRangeAsync(InitialData.Categories);
            await context.SaveChangesAsync();
        }
    }
}
