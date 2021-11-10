using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TestApp.RegistrationEmployee.Core.Interfaces;

namespace TestApp.RegistrationEmployee.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string from, string subject, string body)
        {
            var message = new MailMessage
            {
                From = new MailAddress(from),
                To = { new MailAddress(to) },
                Subject = subject,
                Body = body
            };

            using var emailClient = new SmtpClient("localhost");
            await emailClient.SendMailAsync(message);
            _logger.LogWarning($"Sending email to {to} from {from} with subject {subject}.");
        }
    }
}
