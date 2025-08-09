using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Utils
{
    public static class ResponseConsts
    {
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

        // General
        public const string RequestCompleted = "Solicitud completada con exito";
    }

}
