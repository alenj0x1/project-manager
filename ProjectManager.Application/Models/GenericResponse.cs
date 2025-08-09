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
        public string Message { get; set; } = ResponseConsts.RequestCompleted;
        public int StatusCode { get; set; } = ResponseHttpCodes.Success;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
