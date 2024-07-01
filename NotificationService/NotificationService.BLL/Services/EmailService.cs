using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using NotificationService.BLL.Interfaces;
using NotificationService.BLL.Models;

namespace NotificationService.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger)
        {
            _logger = logger;
            _emailSettings = options.Value;
        }

        public async Task SendAsync(EmailModel emailModel)
        {
            try
            {
                using var email = new MimeMessage();
                email.Sender = new MailboxAddress(_emailSettings.Username, _emailSettings.SenderEmail);
                email.To.Add(MailboxAddress.Parse(emailModel.To));
                email.Subject = emailModel.Subject;

                var bodyBuilder = new BodyBuilder
                {
                    TextBody = emailModel.Body
                };
                email.Body = bodyBuilder.ToMessageBody();

                using var smtpClient = new SmtpClient();
                await smtpClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls, default);
                await smtpClient.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.Password, default);
                await smtpClient.SendAsync(email, default);
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
                throw;
            }
        }
    }
}

