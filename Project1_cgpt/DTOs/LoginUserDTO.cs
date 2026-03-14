using System.ComponentModel.DataAnnotations;

namespace Project1_cgpt.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        public required string UserNameOrEmail { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}