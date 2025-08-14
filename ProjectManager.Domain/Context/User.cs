using System;
using System.Collections.Generic;

namespace ProjectManager.Domain.Context;

public partial class User
{
    public Guid UserId { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string Identification { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public int RoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual ICollection<User> InverseCreatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<User> InverseUpdatedByNavigation { get; set; } = new List<User>();

    public virtual ICollection<Project> ProjectCreatedByNavigations { get; set; } = new List<Project>();

    public virtual ICollection<Project> ProjectUpdatedByNavigations { get; set; } = new List<Project>();

    public virtual ICollection<ProjectsUser> ProjectsUsers { get; set; } = new List<ProjectsUser>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Task> TaskCreatedByNavigations { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskUpdatedByNavigations { get; set; } = new List<Task>();

    public virtual ICollection<Task> TaskUsers { get; set; } = new List<Task>();

    public virtual User? UpdatedByNavigation { get; set; }
}
