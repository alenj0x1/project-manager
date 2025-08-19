using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Utils
{
    public static class ResponseConsts
    {
        // Users
        public const string UserEmailAddressExists =
            "Ya existe un usuario con la misma dirección de correo electrónico";

        public const string UserIdentificationExists =
            "Ya existe un usuario con la misma dirección de correo electrónico";

        public const string UserCreated = "Usuario creado correctamente";
        public const string UserList = "Listado de usuarios";

        // Roles
        public const string RoleNotExists = "El rol no existe";

        // Projects
        public const string ProjectNotFound = "El proyecto no existe";
        public const string ProjectCreated = "Proyecto creado exitosamente";
        public const string ProjectUpdated = "Proyecto actualizado correctamente";
        public const string ProjectDeleted = "Eliminación de proyecto realizada correctamente";
        public const string ProjectSearchCompleted = "Busqueda realizada correctamente";

        // Tasks
        public static string TaskCreated(string projectName)
        {
            return $"Tarea creada con exito en el proyecto: {projectName}";
        }
        
        // Middlewares
        public const string MiddlewareErrorBadRequest = "400 Bad Request";
        public const string MiddlewareErrorNotFound = "404 Not Found";
        public const string MiddlewareErrorUnauthorized = "401 Unauthorized";
        public const string MiddlewareErrorForbidden = "403 Forbidden";
        public const string MiddlewareErrorInternalServerError = "500 Internal Server Error";

        // Jwt
        public const string JwtSecretKeyNotArgumented = "No se argumentó la llave privada de JWT";
        public const string JwtExpirationInMinutesNotArgumented = "No se argumentó la expiración para JWT";
        public const string JwtAudienceNotArgumented = "No se argumentó la audiencia para JWT";
        public const string JwtIssuerNotArguemented = "No se argumentó el Issuer para JWT";

        // General
        public const string RequestCompleted = "Solicitud completada con exito";
        public static string ConfigurationArgumentNotFound(string property) => $"No se argumentó la siguiente propiedad {property} de la configuración";
    }
}