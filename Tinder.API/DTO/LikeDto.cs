namespace Tinder.API.Models
{
    public class LikeDTO
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public UserDto SenderUser { get; set; }
        public Guid ReceiverId { get; set; }
        public UserDto ReceiverUser { get; set; }
    }
}
