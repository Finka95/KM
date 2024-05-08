namespace Tinder.DAL.Entities;
public class ChatEntity : BaseEntity
{
    public List<MessageEntity> Messages { get; set; } 
    public List<Guid> UserIds { get; set; }  
}
