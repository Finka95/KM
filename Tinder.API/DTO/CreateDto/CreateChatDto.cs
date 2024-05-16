using Tinder.API.Models;

namespace Tinder.API.DTO.CreateDto
{
    public class CreateChatDto
    {
        public ICollection<MessageDto> Messages { get; set; }
        public ICollection<UserDto> Users { get; set; }
    }
}
