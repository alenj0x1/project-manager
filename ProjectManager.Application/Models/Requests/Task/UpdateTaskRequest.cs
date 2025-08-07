using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Models.Requests.Task
{
    public class UpdateTaskRequest
    {
        [MaxLength(50, ErrorMessage = "El nombre de la tarea es muy larga")]
        [MinLength(3)]
        public string Name { get; set; }
        [MaxLength(500, ErrorMessage = "La descripción de la tarea es muy larga")]
        [MinLength(10)]
        public string? Description { get; set; }
        public int StatusId { get; set; } = 1;
    }
}
