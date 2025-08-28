using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Models.Requests.Task
{
    public class CreateTaskRequest
    {
        [Required]
        [MaxLength(50)]
        [MinLength(6)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(1000)]
        [MinLength(10)]
        public required string Description { get; set; }
        [Required]
        public required int StatusId { get; set; }
    }
}
