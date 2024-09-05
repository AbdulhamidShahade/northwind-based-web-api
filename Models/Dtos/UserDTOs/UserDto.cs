using System.ComponentModel.DataAnnotations;

namespace NorthwindBasedWebAPI.Models.Dtos.UserDtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Required, Compare(nameof(Password))]
        public string ConfiremPassword { get; set; }
    }
}
