namespace Tinder.API.Hubs.Interfaces
{
    public interface IChatHub
    {
        public Task ReceiveMessage(string userName, string message);
    }
}
