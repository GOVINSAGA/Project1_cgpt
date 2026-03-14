using Project1_cgpt.DTOs;
using Project1_cgpt.Models;

namespace Project1_cgpt.Services
{
    public interface IUserService
    {
        string Register(RegisterUserDTO dto);
        string Login(LoginUserDTO dto);
        User GetProfile(string username);
    }
}