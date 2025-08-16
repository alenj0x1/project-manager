namespace ProjectManager.Application.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }

        public string EmailAddress { get; set; } = null!;

        public string Identification { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public bool IsActive { get; set; }

        public int RoleId { get; set; }
        public RoleDto Role { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }

    }
}
