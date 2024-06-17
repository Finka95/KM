namespace Tinder.API.Models
{
    public class PhotoDto
    {
        public Guid Id { get; set; }
        public string PhotoURL { get; set; }
        public bool IsAvatar { get; set; }
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
    }
}
