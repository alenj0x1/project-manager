using Microsoft.EntityFrameworkCore;
using ProjectManager.Application.Dtos;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Services;
using ProjectManager.Domain.Context;
using ProjectManager.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Services
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITaskService, TaskService>();

// Context
builder.Services.AddNpgsql<PostgresContext>(builder.Configuration.GetConnectionString("Postgres") ?? throw new Exception("No se ha establecido la conexión de Postgres en el archivo appsettings.json"));

// Repositories
builder.Services.AddSingleton<Repository<ProjectDto>>();
builder.Services.AddSingleton<Repository<TaskDto>>();

var message = builder.Configuration["InitialConfiguration:User:Identification"];

Console.WriteLine(message);

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
