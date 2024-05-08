namespace Tinder.DAL.Entities;

public class MessageEntity : BaseEntity
{
    public string Text { get; set; }
    public DateTime Date { get; set; }
    public Guid ChatId { get; set; }
    public Guid UserSenderId { get; set; }

}
