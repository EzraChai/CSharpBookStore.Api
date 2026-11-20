using System.Text;
using BookStore.Api.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookStoreContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("BookStore")
    )
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string jwtKey =
       Environment.GetEnvironmentVariable("JWT_KEY")
       ?? throw new InvalidOperationException("JWT_KEY is missing.");

string jwtIssuer =
  Environment.GetEnvironmentVariable("JWT_ISSUER")
  ?? throw new InvalidOperationException("JWT_ISSUER is missing.");

string jwtAudience =
  Environment.GetEnvironmentVariable("JWT_AUDIENCE")
  ?? throw new InvalidOperationException("JWT_AUDIENCE is missing.");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtKey!)
            )
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
