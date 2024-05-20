using AutoMapper;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.BLL.Services
{
    public class LikeService : GenericService<Like, LikeEntity>, ILikeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;

        public LikeService(ILikeRepository repository, IMapper mapper,
            IUserRepository userRepository, IChatRepository chatRepository)
            : base(repository, mapper)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
        }

        public override async Task<Like> CreateAsync(Like like, CancellationToken cancellationToken)
        {
            var sender = await _userRepository.GetByIdAsync(like.SenderId, cancellationToken);
            var receiver = await _userRepository.GetByIdAsync(like.ReceiverId, cancellationToken);

            if (sender.ReceivedLikes.Any(l => l.SenderId == receiver.Id))
            {
                sender.ReceivedLikes = sender.ReceivedLikes.Where(l => l.SenderId != receiver.Id).ToList();
                receiver.SentLikes = receiver.SentLikes.Where(l => l.ReceiverId != sender.Id).ToList();

                var newChat = new ChatEntity()
                {
                    Users = new List<UserEntity> { sender, receiver },
                    Messages = new List<MessageEntity>(),
                };
                await _chatRepository.CreateAsync(_mapper.Map<ChatEntity>(newChat), cancellationToken);
            }

            var likeEntity = new LikeEntity()
            {
                SenderUser = sender,
                ReceiverUser = receiver,
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
            };

            await _repository.CreateAsync(likeEntity, cancellationToken);
            return _mapper.Map<Like>(likeEntity );
        }
    }
}
