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
    public class GenericServiceTests
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFixture _fixture;

        public GenericServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();

            _fixture = new Fixture();
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _userService = new UserService(_userRepository, _mapper);
        }

        [Fact]
        public async Task GetByIdAsync_ValidId_ReturnUser()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var entity = _fixture
                .Build<UserEntity>()
                .With(e => e.Id, userId)
                .Create();

            _userRepository
                .GetByIdAsync(userId, default)
                .Returns(entity);

            // Act
            var model = await _userService.GetByIdAsync(userId, default);

            // Assert
            model.ShouldNotBeNull();
            model.ShouldBeOfType(typeof(User));
            model.Id.ShouldBe(userId);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ReturnNull()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepository
                .GetByIdAsync(userId, default)
                .ReturnsNull();

            // Act
            var model = await _userService.GetByIdAsync(userId, default);
            
            // Assert
            model.ShouldBeNull();
        }

        [Fact]
        public async Task GetAllAsync_ValidData_ReturnListOfUsers()
        {
            // Arrange
            var entities = _fixture.Build<List<UserEntity>>().Create();
            _userRepository
                .GetAllAsync(default)
                .Returns(entities);
            var models = _mapper.Map<List<User>>(entities);

            // Act
            var result = await _userService.GetAllAsync(default);

            // Assert
            result.ShouldBeEquivalentTo(models);
        }

        [Fact]
        public async Task CreateAsync_ValidUserModel_ShouldCreateUser()
        {
            // Arrange
            var entity = _fixture.Build<UserEntity>().Create();
            var model = _mapper.Map<User>(entity);

            _userRepository
                .CreateAsync(Arg.Any<UserEntity>(),default)
                .Returns(entity);

            // Act
            var result = await _userService.CreateAsync(model, default);

            // Assert
            result.ShouldBeEquivalentTo(model);
        }

        [Fact]
        public async Task CreateAsync_InvalidUserModel_ReturnNull()
        {
            // Arrange
           var model = _fixture.Build<User>().Create();

            _userRepository
                .CreateAsync( Arg.Any<UserEntity>(),default)
                .ReturnsNull();

            // Act
            var result = await _userService.CreateAsync(model, default);

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async Task UpdateAsync_ValidUserModel_ShouldUpdateUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var entity = _fixture
                .Build<UserEntity>()
                .With(e => e.Id, userId).Create();
            var model = _mapper.Map<User>(entity);

            _userRepository
                .GetByIdAsync(userId, default)
                .Returns(entity);

            _userRepository
                .UpdateAsync(Arg.Any<UserEntity>(), default)
                .Returns(entity);
            // Act
            var result = await _userService.UpdateAsync(userId, model, default);

            // Assert
            result.ShouldBeEquivalentTo(model);
        }

        [Fact]
        public async Task UpdateAsync_InvalidUserModel_ShouldUpdateUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var model = _fixture
                .Build<User>()
                .With(e => e.Id, Guid.NewGuid).Create();

            _userRepository
                .GetByIdAsync(userId, default)
                .ReturnsNull();

            // Act
            var action = async () => await _userService.UpdateAsync(userId, model, default);

            // Assert
            action.ShouldThrow<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ValidId_ShouldDeleteUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var entity = _fixture
                .Build<UserEntity>()
                .With(e => e.Id, userId)
                .Create();
            var model = _mapper.Map<User>(entity);

            _userRepository
                .GetByIdAsync(userId, default)
                .Returns(entity);

            _userRepository
                .DeleteByIdAsync(userId, default)
                .Returns(entity);

            // Act
            var result = await _userService.DeleteAsync(userId, default);

            // Assert
            result.ShouldBeEquivalentTo(model);
        }

        [Fact]
        public async Task DeleteAsync_InValidId_ShouldReturnNull()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userRepository
                .GetByIdAsync(userId, default)
                .ReturnsNull();

            // Act
            var action = async() => await _userService.DeleteAsync(userId, default);

            // Assert
            action.ShouldThrow<NotFoundException>();
        }
    }
}
