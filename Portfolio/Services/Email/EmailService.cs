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

            // Crear un cliente SMTP que se encargará de enviar el correo
            using var smtp = new SmtpClient();

            // Conectarse al servidor SMTP usando las configuraciones para el host, puerto y seguridad (StartTls)
            await smtp.ConnectAsync(
                _configuration.GetSection("MailSettings:Host").Value, // Host del servidor SMTP (ej. smtp.gmail.com)
                Convert.ToInt32(_configuration.GetSection("MailSettings:Port").Value), // Puerto del servidor (ej. 587)
                SecureSocketOptions.StartTls // Usar StartTLS para encriptar la conexión
            );

            // Autenticarse en el servidor usando el correo y la contraseña configurados (appsettings.json)
            await smtp.AuthenticateAsync(
                _configuration.GetSection("MailSettings:Mail").Value, // Correo de la cuenta usada para enviar
                _configuration.GetSection("MailSettings:Password").Value // Contraseña de la cuenta (o clave de aplicación de Gmail)
            );

            // Enviar el correo con el cliente SMTP
            await smtp.SendAsync(message);

            // Desconectarse del servidor SMTP después de enviar el correo
            await smtp.DisconnectAsync(true);
        }
    }
}
