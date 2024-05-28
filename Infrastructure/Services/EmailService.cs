using System.Net;
using System.Net.Mail;

namespace octo.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromAddress;

        public EmailService(string smtpHost, int smtpPort, string fromAddress, string smtpUsername, string smtpPassword)
        {
            _smtpClient = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };
            _fromAddress = fromAddress;
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var mailMessage = new MailMessage(_fromAddress, to, subject, body);
            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
