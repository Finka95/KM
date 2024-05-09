namespace Tinder.API.Models
{
    public class ChatDTO
    {
        public Guid Id { get; set; }
        public ICollection<MessageDto> Messages { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}
