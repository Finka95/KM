using AutoFixture;
using AutoMapper;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Shouldly;
using Tinder.BLL.Exceptions;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Mapper;
using Tinder.BLL.Models;
using Tinder.BLL.Services;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.Tests
{
    public class LikeServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IMapper _mapper;
        private readonly ILikeService _likeService;

        public LikeServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _likeRepository = Substitute.For<ILikeRepository>();
            _chatRepository = Substitute.For<IChatRepository>();

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();

            _likeService = new LikeService(_likeRepository, _mapper, _userRepository, _chatRepository);
        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_ValidLikeModelWithMatch_ShouldCreateLikeAndChat(
            Guid senderId,
            Guid receiverId,
            UserEntity senderEntity,
            UserEntity receiverEntity,
            ChatEntity chatEntity,
            LikeEntity senderReceivedLike,
            LikeEntity receiverSentLike,
            LikeEntity likeEntity
            )
        {
            // Arrange
            senderEntity.Id = senderId;
            receiverEntity.Id = receiverId;

            senderReceivedLike.ReceiverId = senderId;
            senderReceivedLike.SenderId = receiverId;

            receiverSentLike.ReceiverId = senderId;
            receiverSentLike.SenderId = receiverId;

            senderEntity.ReceivedLikes.Add(senderReceivedLike);
            receiverEntity.SentLikes.Add(receiverSentLike);

            chatEntity.Users.Clear();
            chatEntity.Users.Add(senderEntity);
            chatEntity.Users.Add(receiverEntity);


            likeEntity.ReceiverId = receiverId;
            likeEntity.SenderId = senderId;
            likeEntity.ReceiverUser = receiverEntity;
            likeEntity.SenderUser = senderEntity;

            var likeModel = _mapper.Map<Like>(likeEntity);
            
            _userRepository.GetByIdAsync(senderId, default).Returns(senderEntity);
            _userRepository.GetByIdAsync(receiverId, default).Returns(receiverEntity);
            _chatRepository.CreateAsync(Arg.Any<ChatEntity>(), default).Returns(chatEntity);
            _likeRepository.CreateAsync(Arg.Any<LikeEntity>(), default).Returns(likeEntity);
            
            // Act
            var result = await _likeService.CreateAsync(likeModel, default);

            // Assert
            await _chatRepository.Received().CreateAsync(Arg.Any<ChatEntity>(), default);
            result.Id.ShouldBeEquivalentTo(likeModel.Id);

        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_ValidLikeModelWithoutMatch_ShouldCreateOnlyLike(
            Guid senderId,
            Guid receiverId,
            UserEntity senderEntity,
            UserEntity receiverEntity,
            LikeEntity likeEntity
            )
        {
            // Arrange
            senderEntity.Id = senderId;
            receiverEntity.Id = receiverId;

            likeEntity.ReceiverId = receiverId;
            likeEntity.SenderId = senderId;
            likeEntity.ReceiverUser = receiverEntity;
            likeEntity.SenderUser = senderEntity;

            var likeModel = _mapper.Map<Like>(likeEntity);

            _userRepository.GetByIdAsync(senderId, default).Returns(senderEntity);
            _userRepository.GetByIdAsync(receiverId, default).Returns(receiverEntity);
            _likeRepository.CreateAsync(Arg.Any<LikeEntity>(), default).Returns(likeEntity);

            // Act
            var result = await _likeService.CreateAsync(likeModel, default);

            // Assert
            await _chatRepository.DidNotReceive().CreateAsync(Arg.Any<ChatEntity>(), default);
            result.Id.ShouldBeEquivalentTo(likeModel.Id);
        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_InvalidLikeModel_ShouldReturnNull(
            Like likeModel
            )
        {
            // Arrange
            _userRepository.GetByIdAsync(Arg.Any<Guid>(), default).ReturnsNull();
            _userRepository.GetByIdAsync(Arg.Any<Guid>(), default).ReturnsNull();
            _likeRepository.CreateAsync(Arg.Any<LikeEntity>(), default).ReturnsNull();

            // Act
            var action = async() => await _likeService.CreateAsync(likeModel, default);

            // Assert
            await _chatRepository.DidNotReceive().CreateAsync(Arg.Any<ChatEntity>(), default);
            action.ShouldThrow<NotFoundException>();
        }
    }
}
