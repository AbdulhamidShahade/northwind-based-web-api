using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Models.Dtos.AuthDtos;
using NorthwindBasedWebAPI.Models.Dtos.UserDtos;
using NorthwindBasedWebApplication.API.Models;
using System.Threading.Tasks;


namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface IAuthenticationRepository
    {
        Task<bool> IsUniqueUser(UserDto user);
        Task<RegisterResponseDto> Register(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<ApplicationUser> GetByUserNameAsync(string name);
        Task<ApplicationUser> GetByEmailAsync(string email);
        bool IsAuthenticatedUser(string? email = null, string? userName = null);

    }
}
