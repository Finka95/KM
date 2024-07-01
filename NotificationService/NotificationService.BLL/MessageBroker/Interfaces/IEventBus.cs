namespace NotificationService.BLL.MessageBroker.Interfaces
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken)
            where T : class;
    }
}
