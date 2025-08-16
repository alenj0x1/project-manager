using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Application.Models.Requests.User
{
    public class UpdateUserRequest
    {
        public class CreateUserRequest
        {
            [EmailAddress]
            [MaxLength(100)]
            [MinLength(8)]
            public string? EmailAddress { get; set; }
            [MaxLength(10)]
            [MinLength(10)]
            public string? Identification { get; set; }
            [MaxLength(60)]
            [MinLength(3)]
            public string? FirstName { get; set; }
            [MaxLength(60)]
            [MinLength(3)]
            public string? LastName { get; set; }
            public int? RoleId { get; set; }
        }
    }
}
