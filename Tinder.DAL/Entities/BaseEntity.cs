using Tinder.DAL.Interfaces;

namespace Tinder.DAL.Entities;

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
}
