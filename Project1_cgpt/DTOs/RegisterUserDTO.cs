using System.ComponentModel.DataAnnotations;

namespace Project1_cgpt.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$")]
        public string MobileNo { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Address { get; set; }
    }
}