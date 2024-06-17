using AutoMapper;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.BLL.Services
{
    public class ChatService : GenericService<Chat, ChatEntity>, IChatService
    {
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatRepository chatRepository, IMapper mapper)
            : base(chatRepository, mapper)
        {
            _chatRepository = chatRepository;
        }
        
        public async Task<Chat> GetByIdWithUsersAndMessagesAsync(Guid id, CancellationToken cancellationToken)
        {
            var chatEntity = await _chatRepository.GetByIdWithUsersAndMessagesAsync(id, cancellationToken);
            return _mapper.Map<Chat>(chatEntity);
        }
    }
}
