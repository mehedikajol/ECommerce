namespace ECommerce.Application.IServices;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string to, string subject, string body);
}
