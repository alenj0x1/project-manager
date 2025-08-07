using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Application.Models.Requests.Project
{
    public class CreateProjectRequest
    {
        [MaxLength(50, ErrorMessage = "El nombre del proyecto es muy largo")]
        [MinLength(3)]
        public required string Name { get; set; }
        [MaxLength(500, ErrorMessage = "La descripción del proyecto es muy larga")]
        [MinLength(10)]
        public required string Description { get; set; }
        public int StatusId { get; set; } = 1;
        [MaxLength(255, ErrorMessage = "El banner del proyecto es muy largo")]
        [MinLength(15)]
        public required string Banner { get; set; }
    }
}
