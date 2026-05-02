using BlueCleanApi.Domains.Interfaces;
using BlueCleanApi.Domains.Services;
using BlueCleanApi.Extensions.Interfaces;
using BlueCleanApi.Extensions.Services;
using BlueCleanApi.Resources;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configurar OpenAPI
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "BlueClean API",
            Version = "v1",
            Description = "API para gerenciamento do sistema BlueClean com autenticação JWT Bearer"
        };

        return Task.CompletedTask;
    });
});

// Configurar HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Injeção de dependências
builder.Services.AddScoped<INotificadorDominio, NotificadorDominio>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// Configurar JWT Authentication
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException(StringResources.JwtKeyNaoConfigurado);
}

var key = Encoding.UTF8.GetBytes(jwtKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            // Previne a resposta padrão
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var message = context.Error == "invalid_token"
                ? StringResources.TokenInvalido
                : context.ErrorDescription?.Contains("expired") == true
                    ? StringResources.TokenExpirado
                    : StringResources.TokenNaoInformado;

            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                Success = false,
                Message = message
            });

            return context.Response.WriteAsync(result);
        },
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Append("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("BlueClean API - Documentação Interativa")
            .WithTheme(ScalarTheme.Purple)
            .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
