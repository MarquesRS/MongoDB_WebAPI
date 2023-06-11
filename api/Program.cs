// dotnet add package MongoDB.Driver --version 2.19.2
using api.Services;
using api.Models;
using api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseModel>(builder.Configuration.GetSection("MongoDb"));

builder.Services.AddSingleton<DatabaseContext>();

builder.Services.AddScoped<IBrandRepository, BrandRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
