using NorthwindBasedWebAPI.Models.Dtos.AuthDtos;
using NorthwindBasedWebAPI.Models.Dtos.UserDtos;
using NorthwindBasedWebApplication.API.Models;


namespace NorthwindBasedWebAPI.Repositories.IRepository
{
    public interface IAuthenticationRepository
    {
        Task<bool> IsUniqueUser(UserDto user);
        Task<RegisterResponseDto> Register(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
