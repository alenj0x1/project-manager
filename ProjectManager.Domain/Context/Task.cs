using System;
using System.Collections.Generic;

namespace ProjectManager.Domain.Context;

public partial class Task
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

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Project? Project { get; set; }

    public virtual TasksStatus Status { get; set; } = null!;

    public virtual User UpdatedByNavigation { get; set; } = null!;

    public virtual User? User { get; set; }
}
