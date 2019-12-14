using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SourceName.Api.Model.Users
{
    public class CreateUserRequest
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(255)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [Required]
        public List<int> RoleIds { get; set; } = new List<int>();
    }
}