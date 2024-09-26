using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Portfolio.Entities;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;

namespace Portfolio.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(MailRequest request)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress($"{request.Name} from Portfolio", request.Email));

            message.To.Add(new MailboxAddress("", _configuration["MailSettings:DestinationEmail"]));

            message.Subject = _configuration["MailSettings:Subject"];
            message.Body = new TextPart("html")
            {
                Text = $"<h2>Sender: {request.Email}</h2> <p>{request.Message}</p>"
            };

            message.Priority = MessagePriority.Urgent;
            message.Headers.Add("X-Email", $"{request.Email}");

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _configuration.GetSection("MailSettings:Host").Value,
                Convert.ToInt32(_configuration.GetSection("MailSettings:Port").Value),
                SecureSocketOptions.StartTls 
            );

            await smtp.AuthenticateAsync(
                _configuration.GetSection("MailSettings:Mail").Value, 
                _configuration.GetSection("MailSettings:Password").Value 
            );

            await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }
    }
}
