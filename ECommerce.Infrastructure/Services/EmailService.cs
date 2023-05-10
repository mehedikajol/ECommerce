using ECommerce.Application.IServices;
using ECommerce.Core.Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace ECommerce.Infrastructure.Services;

internal class EmailService : IEmailService
{
    private readonly EmailServerSetup _emailServerSetup;

    public EmailService(IOptions<EmailServerSetup> options)
    {
        _emailServerSetup = options.Value;
    }

    public async Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_emailServerSetup.UserEmail));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(_emailServerSetup.Host, _emailServerSetup.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailServerSetup.UserEmail, _emailServerSetup.Password);
            await smtp.SendAsync(email);
            smtp.Dispose();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
}
