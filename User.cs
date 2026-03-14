using System.ComponentModel.DataAnnotations;

namespace Project1_cgpt.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string UserName { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 8)]
        public required string Password { get; set; }

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{10}$")]
        public required string MobileNo { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        public string? Address { get; set; }
    }
}