namespace Tinder.BLL.Models
{
    public class Photo
    {
        public Guid Id { get; set; }
        public string PhotoURL { get; set; }
        public bool IsAvatar { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
