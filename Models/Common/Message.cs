using MimeKit;

namespace NorthwindBasedWebAPI.Models.Common
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string content)
        {
            this.Subject = subject;
            this.Content = content;
            this.To = new List<MailboxAddress>();
            this.To.AddRange(to.Select(x => new MailboxAddress("email", x)));
        }
    }
}
