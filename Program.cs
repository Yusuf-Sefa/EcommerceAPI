
using ECommerceAPI.APIContext;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.Run();
