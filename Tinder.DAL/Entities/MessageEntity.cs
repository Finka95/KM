
using Tinder.DAL.Interfaces;

namespace Tinder.DAL.Entities;

public class MessageEntity : IBaseEntity
{
    public Guid Id { get; set; }
    public string Text { get; set; }


    public Guid ChatId { get; set; }
    //public Guid UserReceiverId { get; set; }
}
