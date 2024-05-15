namespace Tinder.API.Models
{
    public class LikeDto
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public UserDto SenderUser { get; set; }
        public Guid ReceiverId { get; set; }
        public UserDto ReceiverUser { get; set; }
    }
}
