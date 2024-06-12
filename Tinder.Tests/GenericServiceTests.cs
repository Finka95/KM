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

        public GenericServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();

            _userService = new UserService(_userRepository, _mapper);
        }

        [Theory, AutoMoqData]
        public async Task GetByIdAsync_ValidId_ReturnUser(
            UserEntity entity
            )
        {
            // Arrange
            var id = Guid.NewGuid();
            entity.Id = id;

            _userRepository
                .GetByIdAsync(id, default)
                .Returns(entity);

            // Act
            var model = await _userService.GetByIdAsync(id, default);

            // Assert
            model.ShouldNotBeNull();
            model.ShouldBeOfType(typeof(User));
            model.Id.ShouldBe(id);
        }

        [Fact]
        public async Task GetByIdAsync_InvalidId_ReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            _userRepository
                .GetByIdAsync(id, default)
                .ReturnsNull();

            // Act
            var model = await _userService.GetByIdAsync(id, default);
            
            // Assert
            model.ShouldBeNull();
        }

        [Theory, AutoMoqData]
        public async Task GetAllAsync_ValidData_ReturnListOfUsers(
            List<UserEntity> entities
            )
        {
            // Arrange
            _userRepository
                .GetAllAsync(default)
                .Returns(entities);
            var models = _mapper.Map<List<User>>(entities);

            // Act
            var result = await _userService.GetAllAsync(default);

            // Assert
            result.ShouldBeEquivalentTo(models);
        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_ValidUserModel_ShouldCreateUser(
            UserEntity entity
            )
        {
            // Arrange
            var model = _mapper.Map<User>(entity);

            _userRepository
                .CreateAsync(Arg.Any<UserEntity>(),default)
                .Returns(entity);

            // Act
            var result = await _userService.CreateAsync(model, default);

            // Assert
            result.ShouldBeEquivalentTo(model);
        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_InvalidUserModel_ReturnNull(
            User model
            )
        {
            // Arrange
            _userRepository
                .CreateAsync( Arg.Any<UserEntity>(),default)
                .ReturnsNull();

            // Act
            var result = await _userService.CreateAsync(model, default);

            // Assert
            result.ShouldBeNull();
        }

        [Theory, AutoMoqData]
        public async Task UpdateAsync_ValidUserModel_ShouldUpdateUser(
            UserEntity entity
            )
        {
            // Arrange
            var id = Guid.NewGuid();
            entity.Id = id;
            var model = _mapper.Map<User>(entity);

            _userRepository
                .GetByIdAsync(id, default)
                .Returns(entity);

            _userRepository
                .UpdateAsync(Arg.Any<UserEntity>(), default)
                .Returns(entity);
            // Act
            var result = await _userService.UpdateAsync(id, model, default);

            // Assert
            result.ShouldBeEquivalentTo(model);
        }

        [Theory, AutoMoqData]
        public void UpdateAsync_InvalidUserModel_ShouldUpdateUser(
            User model
            )
        {
            // Arrange
           var id = Guid.NewGuid();
           model.Id = id;

            _userRepository
                .GetByIdAsync(id, default)
                .ReturnsNull();

            // Act
            var action = async () => await _userService.UpdateAsync(id, model, default);

            // Assert
            action.ShouldThrow<NotFoundException>();
        }

        [Theory, AutoMoqData]
        public async Task DeleteAsync_ValidId_ShouldDeleteUser(
            UserEntity entity)
        {
            // Arrange
            var id = Guid.NewGuid();
            entity.Id = id;
            var model = _mapper.Map<User>(entity);

            _userRepository
                .GetByIdAsync(id, default)
                .Returns(entity);

            _userRepository
                .DeleteByIdAsync(id, default)
                .Returns(entity);

            // Act
            var result = await _userService.DeleteAsync(id, default);

            // Assert
            result.ShouldBeEquivalentTo(model);
        }

        [Fact]
        public void DeleteAsync_InValidId_ShouldReturnNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            _userRepository
                .GetByIdAsync(id, default)
                .ReturnsNull();

            // Act
            var action = async() => await _userService.DeleteAsync(id, default);

            // Assert
            action.ShouldThrow<NotFoundException>();
        }
    }
}
