using ProjectManager.Application.Dtos;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Services;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Interfaces.Repositories;
using ProjectManager.Domain.Repositories;
using ProjectManager.Utils;
using ProjectManager.WebApi.Middlewares;

namespace ProjectManager.WebApi.Extensions;

public static class ServicesExtension
{
    public static void DependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        // Middlewares
        services.AddScoped<ErrorHandlerMiddleware>();

        // Services
        services.AddScoped<IUserService, UserService>();

        // Context
        services.AddNpgsql<PostgresContext>(configuration.GetConnectionString("Postgres") ??
                                                    throw new Exception(
                                                        "No se ha establecido la conexion de Postgres en el archivo appsettings.json"));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
    }
}