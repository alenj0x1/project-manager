using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ProjectManager.Application.Interfaces.Services;
using ProjectManager.Application.Services;
using ProjectManager.Domain.Context;
using ProjectManager.Domain.Exceptions;
using ProjectManager.Domain.Interfaces.Repositories;
using ProjectManager.Domain.Repositories;
using ProjectManager.Utils;
using ProjectManager.WebApi.Middlewares;

namespace ProjectManager.WebApi.Extensions;

public static class ServicesExtension
{
    public static async System.Threading.Tasks.Task DependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAuthentication(builder =>
        {
            builder.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            builder.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(builder =>
        {
            builder.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = configuration["Jwt:Issuer"] ?? throw new TokenHelperJwtException(ResponseConsts.JwtIssuerNotArguemented),
                ValidateAudience = true,
                ValidAudience = configuration["Jwt:Audience"] ?? throw new TokenHelperJwtException(ResponseConsts.JwtAudienceNotArgumented),
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"] ?? throw new TokenHelperJwtException(ResponseConsts.JwtSecretKeyNotArgumented)))
            };
        });

        services.AddAuthorization();

        
        // Middlewares
        services.AddScoped<ErrorHandlerMiddleware>();

        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();

        // Context
        services.AddNpgsql<PostgresContext>(configuration.GetConnectionString("Postgres") ??
                                                    throw new Exception(
                                                        "No se ha establecido la conexion de Postgres en el archivo appsettings.json"));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        // Initialization
        using var scope = services.BuildServiceProvider();
        var userService = scope.GetRequiredService<IUserService>();

        await userService.FirstUser();
    }
}