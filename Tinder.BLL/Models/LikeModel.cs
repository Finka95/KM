namespace Tinder.BLL.Models
{
    public class LikeModel
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public UserModel SenderUser { get; set; }
        public Guid ReceiverId { get; set; }
        public UserModel ReceiverUser { get; set; }

    }
}
