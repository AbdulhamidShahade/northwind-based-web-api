

using NorthwindBasedWebAPI.Models.Dtos.UserDtos;

namespace NorthwindBasedWebAPI.Models.Dtos.AuthDtos
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
