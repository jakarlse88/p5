using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MailKit;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TheCarHub.Models.InputModels;
using TheCarHub.Services;

namespace TheCarHub.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMessageService _messageService;

        public ContactController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        // POST: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(ContactInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", inputModel);
            }

            var messageSuccess = await _messageService.SendEmail(inputModel);

            if (!messageSuccess)
            {
                TempData["MessageFail"] = "There was a problem sending the message. Please try again, or contact the webmaster if the problem persists";
                
                return View("Index", inputModel);
            }

            return RedirectToAction("Index", "Home", inputModel);
        }
    }
}