using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Models
{
    public class GenericResponse<T>
    {
        public required T Data { get; set; }
        public required string Message { get; set; } = "Solicitud procesada con exito";
        public required int StatusCode { get; set; } = 200;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
