using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Models.Requests.Project
{
    public class GetProjectRequest
    {
        [MaxLength(50, ErrorMessage = "El nombre del proyecto es muy largo")]
        [MinLength(3)]
        public string? Name { get; set; }
        [MaxLength(500, ErrorMessage = "La descripción del proyecto es muy larga")]
        [MinLength(10)]
        public string? Description { get; set; }
        public int? StatusId { get; set; }
    }
}
