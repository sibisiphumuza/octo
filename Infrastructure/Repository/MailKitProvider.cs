using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using octo.Presentation.Interface;
using octo.Domain.Model;

namespace octo.Infrastructure.Repository
{
    public class MailKitProvider(IOptions<MailKitOptions> options) : IEmailService
    {
        private readonly MailKitOptions _options = options.Value;

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_options.SenderName, _options.SenderEmail));
            emailMessage.To.Add(new MailboxAddress(toEmail, toEmail));
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = message };
            emailMessage.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                // Connect to the SMTP server
                await client.ConnectAsync(_options.SmtpServer, _options.SmtpPort, MailKit.Security.SecureSocketOptions.StartTlsWhenAvailable);
                // Authenticate
                await client.AuthenticateAsync(_options.SmtpUsername, _options.SmtpPassword);
                // Send email
                await client.SendAsync(emailMessage);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
