using Tinder.DAL.Enums;

namespace Tinder.API.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public Gender Gender { get; set; }

        public ICollection<ChatDTO> Chats { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
        public ICollection<LikeDTO> ReceivedLikes { get; set; }
        public ICollection<LikeDTO> SentLikes { get; set; }
        public ICollection<MessageDto> Messages { get; set; }
    }
}
