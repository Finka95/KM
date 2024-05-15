namespace Tinder.API.Models
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid ChatId { get; set; }
        public ChatDto Chat { get; set; }
        public Guid SenderId { get; set; }
        public UserDto User { get; set; }
    }
}
