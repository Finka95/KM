using MassTransit;
using NotificationService.BLL.Interfaces;
using NotificationService.BLL.Models;
using Shared.Events;

namespace NotificationService.BLL.MessageBroker
{
    public class SubscriptionExpiredNotification : IConsumer<SubscriptionExpired>
    {
        private readonly IEmailService _emailService;

        public SubscriptionExpiredNotification(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task Consume(ConsumeContext<SubscriptionExpired> context)
        {
            Console.WriteLine("SubscriptionExpired is received");
            var email = new EmailModel
            {
                To = context.Message.Email,
                Subject = "Tinder Subscription",
                Body = "Your subscription has expired, please prolong it"
            };
            return _emailService.SendAsync(email);
        }
    }
}
