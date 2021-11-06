using Net6WebApiTemplate.Application.Notifications.Email;

namespace Net6WebApiTemplate.Application.Common.Interfaces
{
    public interface IEmailNotification
    {
        Task SendEmailAsync(EmailMessage message, EmailTemplates template);
    }
}