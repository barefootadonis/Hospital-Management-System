using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Hospital_Management_System.Models;

namespace Hospital_Management_System.Services
{
    public class EmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendAsync(string toEmail, string subject, string body)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_settings.Username);
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;

            using var smtp = new SmtpClient(_settings.Host, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = _settings.EnableSsl
            };

            await smtp.SendMailAsync(message);
        }
    }
}