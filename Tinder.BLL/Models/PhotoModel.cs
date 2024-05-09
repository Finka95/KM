namespace Tinder.BLL.Models
{
    public class PhotoModel
    {
        public Guid Id { get; set; }
        public string PhotoURL { get; set; }
        public bool IsAvatar { get; set; }
        public Guid UserId { get; set; }
        public UserModel User { get; set; }
    }
}
