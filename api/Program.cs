// dotnet add package MongoDB.Driver --version 2.19.2
// dotnet add package Microsoft.AspNetCore.Authentication
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

using api.Services;
using api.Models;
using api.Repositories;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => {
   options.ListenLocalhost(5000);
});

builder.Services.Configure<DatabaseModel>(builder.Configuration.GetSection("MongoDb"));

builder.Services.AddSingleton<DatabaseContext>();

builder.Services.AddSingleton<TokenService>();

builder.Services.AddScoped<IBrandRepository, BrandRepository>();

builder.Services.AddScoped<IModelRepository, ModelRepository>();

builder.Services.AddScoped<ICarRepository, CarRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAuthentication("Bearer").AddJwtBearer( options => {
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = 
        new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretKey"]!)
            ),
            ValidateIssuer = false,
            ValidateAudience = false
        };
});

// Add services to the container.
builder.Services.AddCors();
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

app.UseCors(opt => opt
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
