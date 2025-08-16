using ProjectManager.Utils;
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
        public required string Message { get; set; }
        public required int StatusCode { get; set; }
        public required int Count { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
