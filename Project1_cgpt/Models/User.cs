using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Project1_cgpt.Models
{
    [Index(nameof(UserName), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(MobileNo), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string UserName { get; set; }

        public required string Password { get; set; }

        public DateTime Dob { get; set; }

        public required string MobileNo { get; set; }

        public required string Email { get; set; }

        public required string Address { get; set; }
    }
}