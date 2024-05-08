using Tinder.DAL.Enums;

namespace Tinder.DAL.Entities;

public class UserEntity : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string City { get; set; }
    public string Description { get; set; }
    public Gender Gender { get; set; }

    public ICollection<ChatEntity> Chats { get; set; }
    public ICollection<PhotoEntity> Photos { get; set; }
    public ICollection<LikeEntity> ReceivedLikes { get; set; }
    public ICollection<LikeEntity> SentLikes { get; set; }
    public ICollection<MessageEntity> Messages { get; set; }
}
