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
        private readonly IFixture _fixture;
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

            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _likeService = new LikeService(_likeRepository, _mapper, _userRepository, _chatRepository);
        }

        [Fact]
        public async Task CreateAsync_ValidLikeModelWithMatch_ShouldCreateLikeAndChat()
        {
            // Arrange
            var senderId = Guid.NewGuid();
            var receiverId = Guid.NewGuid();

            var senderEntity = _fixture
                .Build<UserEntity>()
                .With(u => u.Id, senderId)
                .Create();

            var receiverEntity = _fixture
                .Build<UserEntity>()
                .With(u => u.Id, receiverId)
                .Create();

            senderEntity.ReceivedLikes.Add(_fixture
                .Build<LikeEntity>()
                .With(l => l.ReceiverId, senderId)
                .With(l => l.SenderId, receiverId)
                .Create());

            receiverEntity.SentLikes.Add(_fixture
                .Build<LikeEntity>()
                .With(l => l.ReceiverId, senderId)
                .With(l => l.SenderId, receiverId)
                .Create());

            var chatEntity = _fixture
                .Build<ChatEntity>()
                .With(c => c.Users, [senderEntity, receiverEntity])
                .Create();

            var likeEntity = _fixture
                .Build<LikeEntity>()
                .With(l => l.SenderId, senderId)
                .With(l => l.ReceiverId, receiverId)
                .With(l => l.SenderUser, senderEntity)
                .With(l => l.ReceiverUser, receiverEntity)
                .Create();

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

        [Fact]
        public async Task CreateAsync_ValidLikeModelWithoutMatch_ShouldCreateOnlyLike()
        {
            // Arrange
            var senderId = Guid.NewGuid();
            var receiverId = Guid.NewGuid();

            var senderEntity = _fixture
                .Build<UserEntity>()
                .With(u => u.Id, senderId)
                .Create();

            var receiverEntity = _fixture
                .Build<UserEntity>()
                .With(u => u.Id, receiverId)
                .Create();

            var likeEntity = _fixture
                .Build<LikeEntity>()
                .With(l => l.SenderId, senderId)
                .With(l => l.ReceiverId, receiverId)
                .With(l => l.SenderUser, senderEntity)
                .With(l => l.ReceiverUser, receiverEntity)
                .Create();

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

        [Fact]
        public async Task CreateAsync_InvalidLikeModel_ShouldReturnNull()
        {
            // Arrange
            var likeModel = _fixture.Create<Like>();
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
