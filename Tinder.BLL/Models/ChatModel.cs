namespace Tinder.BLL.Models
{
    public class ChatModel
    {
        public Guid Id { get; set; }
        public ICollection<MessageModel> Messages { get; set; }
        public ICollection<UserModel> Users { get; set; }
    }
}
