using BCrypt.Net;
using Project1_cgpt.Data;
using Project1_cgpt.DTOs;
using Project1_cgpt.Models;

namespace Project1_cgpt.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public string Register(RegisterUserDTO dto)
        {
            var exists = _context.Users.Any(u =>
                u.UserName == dto.UserName ||
                u.Email == dto.Email ||
                u.MobileNo == dto.MobileNo);

            if (exists)
                throw new Exception("User already exists");

            int age = DateTime.Today.Year - dto.Dob.Year;

            if (age < 18)
                throw new Exception("User must be at least 18 years old");

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

            return "User registered successfully";
        }

        public string Login(LoginUserDTO dto)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.UserName == dto.UserNameOrEmail ||
                u.Email == dto.UserNameOrEmail);

            if (user == null)
                throw new Exception("Invalid username/email");

            bool passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);

            if (!passwordValid)
                throw new Exception("Invalid password");

            return user.UserName;
        }

        public User GetProfile(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == username);

            if (user == null)
                throw new Exception("User not found");

            return user;
        }
    }
}