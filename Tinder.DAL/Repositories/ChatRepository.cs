using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.DAL.Repositories
{
    public class ChatRepository : GenericRepository<ChatEntity>, IChatRepository
    {
        public ChatRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
