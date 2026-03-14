using System.ComponentModel.DataAnnotations;

namespace Project1_cgpt.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(30)]
        public required string UserName { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 8)]
        public required string Password { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Mobile number must be 10 digits")]
        public required string MobileNo { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(200)]
        public required string Address { get; set; }
    }
}