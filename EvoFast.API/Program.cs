using EvoFast.API;
using EvoFast.API.Extensions;
using EvoFast.Application;
using EvoFast.Infrastructure;
using EvoFast.Infrastructure.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Map OpenAI API Key to environment variable (optional)
var openAiApiKey = builder.Configuration["OpenAiSettings:ApiKey"];
if (!string.IsNullOrEmpty(openAiApiKey))
{
    Environment.SetEnvironmentVariable("OPENAI_API_KEY", openAiApiKey);
}
// Add services to the container.
builder.Services.AddCors(builder.Configuration["AllowedOrigins"]);
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiServices();
// if (app.Environment.IsDevelopment())
// {
    app.MapOpenApi();
    await app.InitialiseDatabaseAsync();
    app.MapScalarApiReference(options =>
    {
        options.Title = "EvoFast API";
        options.ShowSidebar = true;
        options
            .WithPreferredScheme("Bearer")
            .WithHttpBearerAuthentication(bearer =>
            {
                bearer.Token = "your-bearer-token";
            });
    });
// }

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();

public partial class Program { }