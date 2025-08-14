using System;
using System.Collections.Generic;

namespace ProjectManager.Domain.Context;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
