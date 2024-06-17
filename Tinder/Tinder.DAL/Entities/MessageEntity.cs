namespace Tinder.DAL.Entities;

public class MessageEntity : BaseEntity
{
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public Guid ChatId { get; set; }
    public ChatEntity Chat { get; set; }
    public Guid SenderId { get; set; }
    public UserEntity User { get; set; }

}
