using System.ComponentModel.DataAnnotations;

namespace Project1_cgpt.DTOs
{
    public class LoginUserDTO
    {
        [Required]
        public string UserNameOrEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}