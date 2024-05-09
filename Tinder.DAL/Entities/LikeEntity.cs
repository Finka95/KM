namespace Tinder.DAL.Entities;

public class LikeEntity : BaseEntity
{
    public Guid SenderId { get; set; }
    public UserEntity SenderUser { get; set; }
    public Guid ReceiverId { get; set; }
    public UserEntity ReceiverUser { get; set; }

}
