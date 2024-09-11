using Microsoft.AspNetCore.Identity;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Services.IService;

namespace NorthwindBasedWebAPI.Services.Service
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return token;
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }


    }
}
