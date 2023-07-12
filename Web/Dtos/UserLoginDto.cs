using System.ComponentModel.DataAnnotations;

namespace Web.Dtos
{
    public class UserLoginDto
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
