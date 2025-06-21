using EvoFast.API;
using EvoFast.API.Extensions;
using EvoFast.Application;
using EvoFast.Infrastructure;
using EvoFast.Infrastructure.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(builder.Configuration["AllowedOrigins"]);
builder.Services.AddControllers();
builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });
builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);
var app = builder.Build();

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