using Microsoft.AspNetCore.Mvc;
using Project1_cgpt.Data;
using Project1_cgpt.DTOs;
using Project1_cgpt.Models;

namespace Project1_cgpt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterUserDTO dto)
        {
            var exists = _context.Users.Any(u =>
                u.UserName == dto.UserName ||
                u.Email == dto.Email ||
                u.MobileNo == dto.MobileNo);

            if (exists)
            {
                return BadRequest("User already exists");
            }

            int age = DateTime.Today.Year - dto.Dob.Year;

            if (age < 18)
            {
                return BadRequest("User must be at least 18 years old");
            }

            var user = new User
            {
                Name = dto.Name,
                UserName = dto.UserName,
                Password = dto.Password,
                Dob = dto.Dob,
                MobileNo = dto.MobileNo,
                Email = dto.Email,
                Address = dto.Address
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }
    }
}