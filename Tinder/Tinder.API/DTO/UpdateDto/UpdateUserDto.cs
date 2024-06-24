using Tinder.DAL.Enums;

namespace Tinder.API.DTO.UpdateDto
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public Gender Gender { get; set; }
    }
}
