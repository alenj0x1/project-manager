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
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int StatusId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
