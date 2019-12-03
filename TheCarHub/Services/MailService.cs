using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit;
using Microsoft.Extensions.Configuration;

namespace TheCarHub.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void SendEmail(string subject, string text)
        {
            var mimeMessage = new MimeMessage();
            
            mimeMessage.From.Add(new MailboxAddress(
                "Webmaster",
                "noreply@thecarhub.com"));
            
            mimeMessage.To.Add(new MailboxAddress(
                "Admin",
                "karlsen.jonarild@gmail.com"));

            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart("html")
            {
                Text = text
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                
                client.Authenticate(
                    _configuration["Contact:EmailAddress"], 
                    _configuration["Contact:EmailPassword"]);
                
                client.Send(mimeMessage);
                
                client.Disconnect(true);
            }
        }
    }
}