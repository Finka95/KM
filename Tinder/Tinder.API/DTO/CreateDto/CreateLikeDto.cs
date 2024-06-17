namespace Tinder.API.DTO.CreateDto
{
    public class CreateLikeDto
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
}
