using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using UniversalUserAPI.DTOs;

namespace UniversalUserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendEmail(EmailDTO emailDTO)
        {
            if(!ModelState.IsValid) return  BadRequest("Invalid values");

             _emailService.SendEmail(emailDTO);

            return  Ok("Email have been sent :)");
        }

    }
}
