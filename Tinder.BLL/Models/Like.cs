namespace Tinder.BLL.Models
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public User SenderUser { get; set; }
        public Guid ReceiverId { get; set; }
        public User ReceiverUser { get; set; }

    }
}
