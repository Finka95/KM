using Tinder.DAL.Enums;

namespace Tinder.DAL.Entities;

public class UserEntity : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public string UserCity { get; set; }
    public Sex Sex { get; set; }
}
