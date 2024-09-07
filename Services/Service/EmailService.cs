using MimeKit;
using NorthwindBasedWebAPI.Models;
using NorthwindBasedWebAPI.Models.Common;
using NorthwindBasedWebAPI.Services.IService;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;

namespace NorthwindBasedWebAPI.Services.Service
{
    public class EmailService : IEmailService
    {

        private readonly EmailConfiguration _emailConfiguration;
        private readonly UserManager<ApplicationUser> _user;

        public EmailService(EmailConfiguration emailConfiguration, UserManager<ApplicationUser> user)
        {
            _emailConfiguration = emailConfiguration;
            _user = user;
        }

        public void ConfirmEmail(string email)
        {
            throw new NotImplementedException();
        }

        public void ResetPassword(ApplicationUser user, string newPassword, string token)
        {
            throw new NotImplementedException();
        }

        public void SendEmail(MimeMessage message)
        {
            using var client = new SmtpClient();

            try
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);

                client.Send(message);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }

        public async bool SendEmailConfirmed(string emailConfirmed)
        {
            var user = await _user.FindByEmailAsync(emailConfirmed);

            if (user == null)
            {
                return false;
            }

            var emailToConfirm = await _user.ConfirmEmailAsync(user, );
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("email", _emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };

            return emailMessage;
        } 
    }
}
