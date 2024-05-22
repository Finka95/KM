using AutoMapper;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.BLL.Services
{
    public class ChatService : GenericService<Chat, ChatEntity>, IChatService
    {
        public ChatService(IChatRepository chatRepository, IMapper mapper)
            : base(chatRepository, mapper)
        {
            
        }

    }
}
