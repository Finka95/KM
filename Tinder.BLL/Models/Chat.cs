namespace Tinder.BLL.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
