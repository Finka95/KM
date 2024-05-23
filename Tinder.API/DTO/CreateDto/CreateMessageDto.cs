using Tinder.API.Models;
using Tinder.BLL.Models;

namespace Tinder.API.DTO.CreateDto
{
    public class CreateMessageDto
    {
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
        public Guid SenderId { get; set; }
        public User User { get; set; }
    }
}
