using NotificationService.BLL.Models;

namespace NotificationService.BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailModel emailModel);
    }
}
