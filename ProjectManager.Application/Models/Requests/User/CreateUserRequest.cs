using System.ComponentModel.DataAnnotations;

namespace ProjectManager.Application.Models.Requests.User
{
    public class CreateUserRequest
    {
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        [MinLength(8)]
        public required string EmailAddress { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(10)]
        public required string Identification { get; set; }
        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public required string FirstName { get; set; }
        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public required string LastName { get; set; }
        [Required]
        [MaxLength(60)]
        [MinLength(3)]
        public required string Password { get; set; }
        public required int RoleId { get; set; }
    }
}
