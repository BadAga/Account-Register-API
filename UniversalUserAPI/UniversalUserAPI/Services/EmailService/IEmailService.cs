using UniversalUserAPI.DTOs;

namespace UniversalUserAPI.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDTO request);
    }
}
