using System.Reflection;
using DtoAbstractLayer;
using LibraryDTO;
using Microsoft.OpenApi.Models;
using MyLibraryManager;
using OpenLibraryClient;
using StubbedDTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDtoManager,OpenLibClientAPI>();
builder.Services.AddControllers();

var dtoManager = Environment.GetEnvironmentVariable("OPEN_LIBRARY");
var dbDataBase = Environment.GetEnvironmentVariable("DB_DATABASE");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
var dbConnection = $"server={dbServer};port=3306;user={dbUser};password={dbPassword};database={dbDataBase}";

Console.WriteLine($"JJJJJJJJJJJJAJAJAJAJAJAJAJAJAJAJAJAAAAAAAAAAAAAALBLBHQHUDS{dbConnection}");

switch (dtoManager)
{
    case "Stub":
        builder.Services.AddSingleton<IDtoManager, Stub>();
        break;
    case "Api":
        builder.Services.AddSingleton<IDtoManager, OpenLibClientAPI>();
        break;
    case "ApiBdd":
        builder.Services.AddSingleton<IDtoManager, MyLibraryMgr>();
        break;
    case "Bdd":
        builder.Services.AddSingleton<IDtoManager, MyLibraryMgr>(provider => new MyLibraryMgr(dbConnection));
        break;
    default:
        builder.Services.AddSingleton<IDtoManager, Stub>();
        break;
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();