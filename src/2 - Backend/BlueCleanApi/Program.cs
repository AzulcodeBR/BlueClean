using BlueCleanApi.Extensions;
using BlueCleanApi.Models.BlueCleanDb;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "BlueClean API",
            Version = "v1",
            Description = "API para gerenciamento do sistema BlueClean"
        };

        return Task.CompletedTask;
    });
});

builder.Services.AddHttpContextAccessor();

// Configurar DbContext
builder.Services.AddDbContext<LavanderiaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LavanderiaDb")));

builder.Services.AddAutoDiscoveredServices();
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("BlueClean API")
            .WithTheme(ScalarTheme.BluePlanet)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
