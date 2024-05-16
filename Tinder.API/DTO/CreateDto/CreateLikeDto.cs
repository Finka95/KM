using Tinder.API.Models;

namespace Tinder.API.DTO.CreateDto
{
    public class CreateLikeDto
    {
        public Guid SenderId { get; set; }
        //public UserDto SenderUser { get; set; }
        public Guid ReceiverId { get; set; }
        //public UserDto ReceiverUser { get; set; }
    }
}
