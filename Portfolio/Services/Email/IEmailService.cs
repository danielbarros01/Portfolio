using Portfolio.Entities;

namespace Portfolio.Services.Email
{
    public interface IEmailService
    {
        Task SendEmail(MailRequest request);
    }
}
