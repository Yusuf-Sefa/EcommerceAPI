
using DotNetEnv;
using ECommerceAPI.APIContext;
using ECommerceAPI.Repository;
using ECommerceAPI.Repository.RepositoryInterfaces;
using ECommerceAPI.Services;
using ECommerceAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddScoped(typeof(IEnumerableRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IQueryableRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));

builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();
