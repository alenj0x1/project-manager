using System;
using System.Collections.Generic;

namespace ProjectManager.Domain.Context;

public partial class ProjectsStatus
{
    public int ProjectStatusId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
