namespace Tinder.DAL.Entities;
public class ChatEntity : BaseEntity
{
    public ICollection<MessageEntity> Messages { get; set; }
    public ICollection<UserEntity> Users { get; set; }
}
