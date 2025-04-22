using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Repositories.Implementations;
using WebAPI.Repositories;
using WebAPI.Services.Abstractions;
using WebAPI.Mapping;
using AutoMapper;
using WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Services.Implementations;
using WebAPI.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<ProductManagerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductMgrConnectionString"));
});
// Add services to the container.
builder.Services.AddDbContext<ProductMgrAuthDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ProductMgrAuthConnectionString"));
});
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    });

var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
