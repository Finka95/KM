using Tinder.DAL.Enums;

namespace Tinder.BLL.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public Gender Gender { get; set; }

        public ICollection<ChatModel> Chats { get; set; }
        public ICollection<PhotoModel> Photos { get; set; }
        public ICollection<LikeModel> ReceivedLikes { get; set; }
        public ICollection<LikeModel> SentLikes { get; set; }
        public ICollection<MessageModel> Messages { get; set; }
    }
}
