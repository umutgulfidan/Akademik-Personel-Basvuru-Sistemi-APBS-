using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers;
using Core.DependencyResolvers;
using Core.Extensions;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Extensions.Exception;
using Serilog;
using Core.CrossCuttingConcerns.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacBusinessModule());
});

// Serilog yap�land�rmas�
// LogHelper ile Serilog yap�land�rmas�n� �a��r�yoruz
LogHelper.ConfigureLogging();

// Serilog'u ASP.NET Core ile entegre ediyoruz
builder.Host.UseSerilog();

// Inbound claim mapping temizle
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Add services to the container.
builder.Services.AddControllers();

// Swagger
builder.Services.AddSwaggerGen();


// JWT Authentication
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.Jwt.TokenOptions>();

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
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
        NameClaimType = ClaimTypes.Name,
        RoleClaimType = ClaimTypes.Role
    };
});


// Core Dependency Resolvers
builder.Services.AddDependencyResolvers(new ICoreModule[] {new CoreModule()});

// Singletons
//builder.Services.AddSingleton<IAuthService, AuthManager>();
//builder.Services.AddSingleton<ITokenHelper, JwtHelper>();
//builder.Services.AddSingleton<IUserService, UserManager>();
//builder.Services.AddSingleton<IUserDal, EfUserDal>();

var app = builder.Build();

// Yazd���m�z middleware
app.ConfigureCustomExceptionMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Akademik Personel Ba�vuru Sistemi V 0.5");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyHeader());

// Middleware'leri kullan�yoruz
// Loglama
app.UseMiddleware<ExceptionLoggingMiddleware>();  // Hatalar� loglamak i�in
app.UseMiddleware<RequestLoggingMiddleware>();    // �stekleri loglamak i�in


app.UseHttpsRedirection();

// Ensure authentication and authorization are in the correct order
app.UseAuthentication(); // JWT middleware
app.UseAuthorization();  // Authorization middleware

app.MapControllers();

app.Run();
