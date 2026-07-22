
using System.Text;
using DotNetEnv;
using ECommerceAPI.APIContext;
using ECommerceAPI.Entities;
using ECommerceAPI.Mapping;
using ECommerceAPI.Repository;
using ECommerceAPI.Repository.RepositoryInterfaces;
using ECommerceAPI.Services;
using ECommerceAPI.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var host = Env.GetString("DB_HOST");
var port = Env.GetString("DB_PORT");
var dbName = Env.GetString("DB_NAME");
var user = Env.GetString("DB_USER");
var pass = Env.GetString("DB_PASS");

var connectionString = $"Host={host};Port={port};Database={dbName};Username={user};Password={pass}";

builder.Services.AddDbContext<Context>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidIssuer = builder.Configuration["AppSettings:Issuer"],
           ValidateAudience = true,
           ValidAudience = builder.Configuration["AppSettings:Audience"],
           ValidateLifetime  = true,
           IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)),
                ValidateIssuerSigningKey = true
       };
    });

builder.Services.AddAutoMapper(typeof(BrandMappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<IBrandService>();

builder.Services.AddScoped(typeof(IEnumerableRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IQueryableRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));

builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
