using Project1_cgpt.DTOs;
using Project1_cgpt.Models;
using Project1_cgpt.DTOs;
namespace Project1_cgpt.Services
{
    public interface IUserService
    {
        string Register(RegisterUserDTO dto);
        string Login(LoginUserDTO dto);
        ProfileResponseDTO GetProfile(string username);
    }
}