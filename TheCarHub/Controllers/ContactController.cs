using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MailKit;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TheCarHub.Models.InputModels;
using IMailService = TheCarHub.Services.IMailService;

namespace TheCarHub.Controllers
{
    public class ContactController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMailService _mailService;

        public ContactController(IWebHostEnvironment webHostEnvironment, 
            IMailService mailService)
        {
            _webHostEnvironment = webHostEnvironment;
            _mailService = mailService;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        // POST: 
        [HttpPost]
        public string SendEmail([Bind] QuickContactInputModel inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.SenderName) || 
                string.IsNullOrWhiteSpace(inputModel.SenderEmail) ||
                string.IsNullOrWhiteSpace(inputModel.Message))
                return null;
                
            try
            {
                var text = FormatMessageBody(inputModel.SenderName, inputModel.SenderEmail, inputModel.Message);

                _mailService.SendEmail("Contact Form Inquiry", text);
                return "Message sent!";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private string FormatMessageBody(string senderName, string senderEmail, string message)
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

//                1: from
//                2: sent
//                3: to
//                4: subject
//                5: body
            var messageBody = string.Format(builder.HtmlBody,
                senderName,
                String.Format("{0:dddd, d MMM yyyy})", DateTime.Now),
                "karlsen.jonarild@gmail.com",
                "Contact Form Enquiry",
                message);

            return messageBody;
        }
    }
}