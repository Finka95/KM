using AutoMapper;
using Shared.Enums;
using Tinder.Bll.Exceptions;
using Tinder.BLL.Exceptions;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;
using Tinder.BLL.Providers.Interfaces;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.BLL.Services
{
    public class LikeService : GenericService<Like, LikeEntity>, ILikeService
    {
        private const int LikeAmountDayLimit = 2;

        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly ICacheService _cacheService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public LikeService(ILikeRepository repository, IMapper mapper,
            IUserRepository userRepository, IChatRepository chatRepository,
            ICacheService cacheService, IDateTimeProvider dateTimeProvider)
            : base(repository, mapper)
        {
            _likeRepository = repository;
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _cacheService = cacheService;
            _dateTimeProvider = dateTimeProvider;
        }

        public override async Task<Like> CreateAsync(Like like, CancellationToken cancellationToken)
        {
            var utcNow = _dateTimeProvider.UtcNow;

            var sender = await _userRepository.GetByIdAsync(like.SenderId, cancellationToken);
            var senderSubscription = await _cacheService.GetAsync<Subscription>(sender.SubscriptionId.ToString(), cancellationToken) 
                                     ?? throw new BadRequestException("Your subscription has expired");

            var senderSentLikes = await _likeRepository.GetAllUserSentLikesAsync(sender.Id, cancellationToken);
            var senderSentLikesTodayAmount = senderSentLikes.Where(l => l.CreatedAt.Date == utcNow.Date).ToList().Count;

            if (senderSubscription.SubscriptionType == SubscriptionType.Base && senderSentLikesTodayAmount >= LikeAmountDayLimit)
            {
                throw new BadRequestException("User has used up the daily limit");
            }

            var receiver = await _userRepository.GetByIdAsync(like.ReceiverId, cancellationToken);

            if (sender is null || receiver is null)
            {
                throw new NotFoundException("Invalid like model");
            }

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

            var likeEntity = new LikeEntity
            {
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                CreatedAt = utcNow
            };

            var newLike = await _repository.CreateAsync(likeEntity, cancellationToken);
            return _mapper.Map<Like>(newLike);
        }
    }
}
