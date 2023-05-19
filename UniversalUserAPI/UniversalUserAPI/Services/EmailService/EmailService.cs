using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using UniversalUserAPI.DTOs;
//using System.Net.Mail;
using MailKit.Net.Smtp;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Net;

namespace UniversalUserAPI.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(EmailDTO request)
        {
            var email = new MimeMessage();
            String? username = _configuration.GetValue<string>("Mailing:EmailUsername");
            String? password = _configuration.GetValue<string>("Mailing:EmailPassword");
            String? host = _configuration.GetValue<string>("Mailing:EmailHost");


            email.From.Add(MailboxAddress.Parse(username));
            email.To.Add(MailboxAddress.Parse(request.To));

            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html)
            { Text = request.Body };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(host, 587, SecureSocketOptions.StartTls); //for different emails differtent hosts name and posts
                smtp.Authenticate(username, password);
                smtp.Send(email);
                smtp.Disconnect(true);
            };

            //var client = new SmtpClient(host, 587)
            //{
            //    EnableSsl = true,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential(username, password)
            //};

            //client.SendMailAsync(new MailMessage(from: username, 
            //                                    to: request.To, 
            //                                    request.Subject, 
            //                                    request.Body));
        }
    }
}
