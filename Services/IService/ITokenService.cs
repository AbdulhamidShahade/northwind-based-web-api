using NorthwindBasedWebAPI.Models;

namespace NorthwindBasedWebAPI.Services.IService
{
    public interface ITokenService
    {
        Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);

    }
}
