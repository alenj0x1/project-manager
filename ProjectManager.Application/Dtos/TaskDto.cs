using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.Application.Dtos
{
    public class TaskDto
    {
        public Guid TaskId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int StatusId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProjectId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
}
