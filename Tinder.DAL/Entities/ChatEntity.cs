namespace Tinder.DAL.Entities;

internal class ChatEntity : BaseEntity
{
    public List<MessageEntity> Messages { get; set; }
}
