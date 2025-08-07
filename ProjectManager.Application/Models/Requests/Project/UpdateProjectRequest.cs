using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Application.Models.Requests.Project
{
    public class UpdateProjectRequest
    {
        [MaxLength(50, ErrorMessage = "El nombre del proyecto es muy largo")]
        [MinLength(3)]
        public string? Name { get; set; }
        [MaxLength(500, ErrorMessage = "La descripción del proyecto es muy larga")]
        [MinLength(10)]
        public string? Description { get; set; }
        public int? StatusId { get; set; }
        [MaxLength(255, ErrorMessage = "El banner del proyecto es muy largo")]
        [MinLength(15)]
        public string? Banner { get; set; }
    }
}
