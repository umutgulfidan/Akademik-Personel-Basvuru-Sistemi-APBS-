using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.Jwt;
using DataAccess.Concretes;
using Entities.Concretes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

// Identity Options
builder.Services.Configure<IdentityOptions>(options =>
{
    // �ifre gereksinimlerini kald�r�yoruz
    options.Password.RequireDigit = false;            // Say� gereksinimini kald�r
    options.Password.RequireLowercase = false;        // K���k harf gereksinimini kald�r
    options.Password.RequireNonAlphanumeric = false;  // �zel karakter gereksinimini kald�r
    options.Password.RequireUppercase = false;        // B�y�k harf gereksinimini kald�r
    options.Password.RequiredLength = 6;               // �ifre uzunlu�unu 6 karaktere ayarlad�k, opsiyonel
    options.Password.RequiredUniqueChars = 1;          // Tek benzersiz karakter gereksinimini kald�r
});



// Token
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();


var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<Core.Utilities.Security.Jwt.TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });


// Singletons

builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddSingleton<ITokenHelper, JwtHelper>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Akademik Personel Ba�vuru Sistemi V 0.5");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
