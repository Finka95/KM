namespace Tinder.DAL.Entities;

public class LikeEntity : BaseEntity
{
    public Guid LikeSenderId { get; set; }
    public Guid LikeReceiverId { get; set; }
}
