namespace Tinder.API.Models
{
    public class ChatDto
    {
        public Guid Id { get; set; }
        public ICollection<MessageDto> Messages { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}
