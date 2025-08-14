using System;
using System.Collections.Generic;

namespace ProjectManager.Domain.Context;

public partial class ProjectsUser
{
    public int ProjectUserId { get; set; }

    public Guid ProjectId { get; set; }

    public Guid UserId { get; set; }

    public DateTime AddedAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
