using Microsoft.AspNetCore.Mvc;
using Project1_cgpt.Data;
using Project1_cgpt.DTOs;
using Project1_cgpt.Models;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace Project1_cgpt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public UserController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Dob = dto.Dob,
                MobileNo = dto.MobileNo,
                Email = dto.Email,
                Address = dto.Address
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserDTO dto)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.UserName == dto.UserNameOrEmail ||
                u.Email == dto.UserNameOrEmail);

            if (user == null)
            {
                return BadRequest("Invalid username/email");
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!passwordValid)
            {
                return BadRequest("Invalid password");
            }

            var claims = new[]
{
    new Claim(ClaimTypes.Name, user.UserName),
    new Claim(ClaimTypes.Email, user.Email)
};

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenString
            });
        }
    }
}