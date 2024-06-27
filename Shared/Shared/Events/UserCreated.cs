namespace Shared.Events
{
    public class UserCreated
    {
        public Guid FusionUserId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
