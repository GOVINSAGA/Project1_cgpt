using System.ComponentModel.DataAnnotations;

namespace Project1_cgpt.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
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