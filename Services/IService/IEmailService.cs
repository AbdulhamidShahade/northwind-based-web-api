using MimeKit;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Models.Common;

namespace NorthwindBasedWebAPI.Services.IService
{
    public interface IEmailService
    {
        void SendEmail(MimeMessage message);
        void SendEmailConfirmed(string emailConfirmed);
        void ResetPassword(ApplicationUser user, string newPassword, string token);
        void ConfirmEmail(string email);

    }
}
