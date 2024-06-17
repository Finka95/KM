namespace Tinder.DAL.Entities;

public class PhotoEntity : BaseEntity
{
    public string PhotoURL { get; set; }
    public bool IsAvatar { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
}
