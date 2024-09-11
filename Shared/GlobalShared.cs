using Microsoft.AspNetCore.Identity;
using NorthwindBasedWebAPI.Models;

namespace NorthwindBasedWebAPI.Shared
{
    public class GlobalShared
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GlobalShared(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string> GetUserRole(string? email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return "NULL";
            }

            return _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
        }
    }
}
