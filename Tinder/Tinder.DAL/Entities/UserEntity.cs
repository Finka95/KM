using Tinder.DAL.Enums;

namespace Tinder.DAL.Entities;

public class UserEntity : BaseEntity
{
    public Guid FusionUserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string City { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public Guid SubscriptionId { get; set; }

    public ICollection<ChatEntity> Chats { get; set; }
    public ICollection<PhotoEntity> Photos { get; set; }
    public ICollection<LikeEntity> ReceivedLikes { get; set; }
    public ICollection<LikeEntity> SentLikes { get; set; }
    public ICollection<MessageEntity> Messages { get; set; }
}
