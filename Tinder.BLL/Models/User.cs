using Tinder.DAL.Enums;

namespace Tinder.BLL.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public Guid FusionId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Gender Gender { get; set; }

        public ICollection<Chat> Chats { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Like> ReceivedLikes { get; set; }
        public ICollection<Like> SentLikes { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
