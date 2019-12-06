using System;
using System.IO;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using TheCarHub.Models.InputModels;

namespace TheCarHub.Services
{
    public class MessageService : IMessageService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MessageService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public Task<bool> SendEmail(ContactInputModel inputModel)
        {
            if (inputModel == null)
            {
                return Task.FromResult(false);
            }
            
            try
            {
                var mimeMessage = new MimeMessage();
            
                mimeMessage.From.Add(new MailboxAddress(
                    "Webmaster",
                    "noreply@thecarhub.com"));
            
                mimeMessage.To.Add(new MailboxAddress(
                    "Admin",
                    "karlsen.jonarild@gmail.com"));

                mimeMessage.Subject = inputModel.Subject;

                mimeMessage.Body = new TextPart("html")
                {
                    Text = FormatMessageBody(
                        inputModel.SenderName, 
                        inputModel.SenderEmail, 
                        inputModel.SenderPhoneNumber,
                        inputModel.Subject,
                        inputModel.Message)
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

                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult(false);
            }
        }
        
        private string FormatMessageBody(string senderName, string senderEmail, string senderPhone, string subject, string message)
        {
            var templatePath =
                _webHostEnvironment.WebRootPath +
                Path.DirectorySeparatorChar.ToString() +
                "Templates" +
                Path.DirectorySeparatorChar.ToString() +
                "ContactFormEmailTemplate.html";

            var builder = new BodyBuilder();

            using (var sourceReader = System.IO.File.OpenText(templatePath))
            {
                builder.HtmlBody = sourceReader.ReadToEnd();
            }

            // 1: sender name
            // 2: sender email
            // 3: sender telephone
            // 4: sent date
            // 5: to
            // 6: subject
            // 7: body
            var messageBody = string.Format(builder.HtmlBody,
                senderName,
                senderEmail,
                senderPhone,
                String.Format("{0:dddd, d MMM yyyy})", DateTime.Now),
                "karlsen.jonarild@gmail.com",
                subject,
                message);

            return messageBody;
        }
    }
}