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
    public class PhotoServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        public PhotoServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _photoRepository = Substitute.For<IPhotoRepository>();

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            }).CreateMapper();

            _photoService = new PhotoService(_photoRepository, _mapper, _userRepository);
        }
            
        [Theory, AutoMoqData]
        public async Task CreateAsync_ValidRequestUserHasAvatar_ShouldCreateNewAvatarAndUpdatePreviousAvatar(
            Photo photoModel,
            UserEntity userEntity,
            PhotoEntity previousAvatarEntity
            )
        {
            // Arrange
            var userId = Guid.NewGuid();

            previousAvatarEntity.IsAvatar = true;
            previousAvatarEntity.UserId = userId;

            photoModel.IsAvatar = true;
            photoModel.UserId = userId;

            var photoEntity = _mapper.Map<PhotoEntity>(photoModel);

            userEntity.Photos.Clear();
            userEntity.Id = userId;
            userEntity.Photos.Add(previousAvatarEntity);

            _userRepository.GetByIdAsync(userId, default)
                .Returns(userEntity);
            _photoRepository.UpdateAsync(Arg.Any<PhotoEntity>(), default)
                .Returns(previousAvatarEntity);
            _photoRepository.CreateAsync(Arg.Any<PhotoEntity>(), default)
                .Returns(photoEntity);

            // Act
            var result = await _photoService.CreateAsync(userId, photoModel, default);
            
            // Assert
            await _photoRepository
                .Received(1)
                .UpdateAsync(Arg.Is<PhotoEntity>(p => !p.IsAvatar), default);
            await _photoRepository
                .Received(1)
                .CreateAsync(Arg.Is<PhotoEntity>(p => p.UserId == userId), default);
            result.IsAvatar.ShouldBeTrue();

        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_ValidRequestUserDoesNotHaveAvatar_ShouldCreateNewAvatarWithoutUpdatingPreviousAvatar(
            Photo photoModel,
            UserEntity userEntity,
            PhotoEntity nonAvatarPhotoEntity
            )
        {
            // Arrange
            var userId = Guid.NewGuid();

            nonAvatarPhotoEntity.IsAvatar = false;
            nonAvatarPhotoEntity.UserId = userId;

            photoModel.IsAvatar = true;
            photoModel.UserId = userId;

            var photoEntity = _mapper.Map<PhotoEntity>(photoModel);

            userEntity.Photos.Clear();
            userEntity.Id = userId;
            userEntity.Photos.Add(nonAvatarPhotoEntity);

            _userRepository.GetByIdAsync(userId, default)
                .Returns(userEntity);
            _photoRepository.CreateAsync(Arg.Any<PhotoEntity>(), default)
                .Returns(photoEntity);

            // Act
            var result = await _photoService.CreateAsync(userId, photoModel, default);

            // Assert
            await _photoRepository
                .DidNotReceive()
                .UpdateAsync(Arg.Any<PhotoEntity>(), default);
            await _photoRepository
                .Received(1)
                .CreateAsync(Arg.Is<PhotoEntity>(p => p.UserId == userId), default);
            result.IsAvatar.ShouldBeTrue();

        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_ValidRequest_ShouldCreateNonAvatarPhoto(
            Photo photoModel,
            UserEntity userEntity
            )
        {
            // Arrange
            var userId = Guid.NewGuid();

            photoModel.IsAvatar = false;
            photoModel.Id = userId;

            userEntity.Id = userId;

            var photoEntity = _mapper.Map<PhotoEntity>(photoModel);

            _userRepository.GetByIdAsync(userId, default).Returns(userEntity);
            _photoRepository
                .CreateAsync(Arg.Any<PhotoEntity>(), default)
                .Returns(photoEntity);
            
            // Act
            var result = await _photoService.CreateAsync(userId, photoModel, default);

            // Assert
            result.IsAvatar.ShouldBeFalse();
            await _photoRepository.DidNotReceive()
                .UpdateAsync(Arg.Any<PhotoEntity>(), default);
            await _photoRepository
                .Received(1)
                .CreateAsync(Arg.Is<PhotoEntity>(p => p.UserId == userId), default);
        }

        [Theory, AutoMoqData]
        public async Task CreateAsync_InvalidId_ShouldThrowException(
            Photo photoModel
            )
        {
            // Arrange
            var userId = Guid.NewGuid();

            _userRepository.GetByIdAsync(userId, default).ReturnsNull();
            
            // Act
            var action = async () => await _photoService
                .CreateAsync(userId, photoModel, default);

            // Assert
            await _photoRepository.DidNotReceive().UpdateAsync(Arg.Any<PhotoEntity>(), default);
            await _photoRepository.DidNotReceive().CreateAsync(Arg.Any<PhotoEntity>(), default);
            action.ShouldThrow<NotFoundException>();
        }

        [Theory, AutoMoqData]
        public async Task GetByIdAsync_ValidRequest_ShouldReturnPhoto(
            PhotoEntity photoEntity
            )
        {
            // Arrange 
            var userId = Guid.NewGuid();
            var photoId = Guid.NewGuid();

            photoEntity.UserId = userId;
            photoEntity.Id = photoId;

            var photoModel = _mapper.Map<Photo>(photoEntity);

            _photoRepository
                .GetByIdAsync(userId, photoId, default)
                .Returns(photoEntity);

            // Act
            var result = await _photoService.GetByIdAsync(photoId, userId, default);

            // Assert
            result.ShouldBeEquivalentTo(photoModel);
        }

        [Fact]
        public void GetByIdAsync_InvalidRequest_ShouldThrowException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var photoId = Guid.NewGuid();

            _photoRepository.GetByIdAsync(userId, photoId, default).ReturnsNull();

            // Act
            var action = async () => await _photoService.GetByIdAsync(photoId, userId, default);
            
            // Assert
            action.ShouldThrow<NotFoundException>();
        }

        [Theory, AutoMoqData]
        public async Task DeleteAsync_ValidRequest_ShouldDeletePhoto(
             PhotoEntity photoEntity
            )
        {
            // Arrange
            var userId = Guid.NewGuid();
            var photoId = Guid.NewGuid();

            photoEntity.UserId = userId;
            photoEntity.Id = photoId;

            var photoModel = _mapper.Map<Photo>(photoEntity);
            _photoRepository.GetByIdAsync(photoId, userId, default).Returns(photoEntity);

            // Act
            var result = await _photoService.DeleteAsync(userId, photoId, default);

            // Assert
            await _photoRepository.Received(1).DeleteAsync(photoEntity, default);
            result.ShouldBeEquivalentTo(photoModel);
        }

        [Fact]
        public void DeleteAsync_InvalidRequest_ShouldThrowException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var photoId = Guid.NewGuid();

            _photoRepository.GetByIdAsync(photoId, userId, default).ReturnsNull();

            // Act
            var result = async () => await _photoService.DeleteAsync(userId, photoId, default);

            // Assert
            _photoRepository.DidNotReceive().DeleteAsync(Arg.Any<PhotoEntity>(), default);
            result.ShouldThrow<NotFoundException>();
        }

        [Theory, AutoMoqData]
        public async Task UpdateAvatarAsync_ValidRequestUserHasAvatar_ShouldCreateNewAvatar(
            UserEntity userEntity,
            PhotoEntity previousAvatarEntity,
            PhotoEntity photoEntity
            )
        {
            // Arrange
            var userId = Guid.NewGuid();
            var photoId = Guid.NewGuid();

            userEntity.Id = userId;

            photoEntity.Id = photoId;
            photoEntity.UserId = userId;
            photoEntity.IsAvatar = false;

            previousAvatarEntity.IsAvatar = true;
            previousAvatarEntity.UserId = userId;

            userEntity.Photos.Clear();
            userEntity.Photos.Add(previousAvatarEntity);
            userEntity.Photos.Add(photoEntity);

            _userRepository.GetByIdAsync(userId, default).Returns(userEntity);
            _photoRepository.GetByIdAsync(photoId, userId, default).Returns(photoEntity);
            _photoRepository
                .UpdateRangeAsync(Arg.Any<List<PhotoEntity>>(), default)
                .Returns([previousAvatarEntity, photoEntity]);

            // Act
            var result = await _photoService.UpdateAvatarAsync(userId, photoId, default);

            // Assert
            result.IsAvatar.ShouldBeTrue();
            await _photoRepository
                .Received(1)
                .UpdateRangeAsync(Arg.Is<List<PhotoEntity>>(photoEntities =>
                    photoEntities.Count == 2 &&
                    photoEntities.Any(p => p.Id == photoId && p.IsAvatar) &&
                    photoEntities.Any(p => p.Id != photoId && !p.IsAvatar)), default);
        }

        [Theory, AutoMoqData]
        public async Task UpdateAvatarAsync_ValidRequestUserDoesNotHaveAvatar_ShouldCreateAvatar(
            UserEntity userEntity,
            PhotoEntity photoEntity
            )
        {
            // Arrange
            var userId = Guid.NewGuid();
            var photoId = Guid.NewGuid();

            userEntity.Id = userId;

            photoEntity.Id = photoId;
            photoEntity.UserId = userId;
            photoEntity.IsAvatar = false;

            userEntity.Photos.Clear();
            userEntity.Photos.Add(photoEntity);

            _userRepository.GetByIdAsync(userId, default).Returns(userEntity);
            _photoRepository.GetByIdAsync(photoId, userId, default).Returns(photoEntity);
            
            // Act
            var result = await _photoService.UpdateAvatarAsync(userId, photoId, default);
            
            // Assert
            await  _photoRepository
                .Received(1)
                .UpdateAsync(Arg.Is<PhotoEntity>(p => p.Id == photoId), default);
            result.IsAvatar.ShouldBeTrue();
        }

        [Fact]
        public void UpdateAvatarAsync_InvalidUserId_ShouldThrowException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var photoId = Guid.NewGuid();

            _userRepository.GetByIdAsync(userId, default).ReturnsNull();

            // Act
            var action = async () => await _photoService.UpdateAvatarAsync(userId, photoId, default);

            // Assert
            action.ShouldThrow<NotFoundException>();
            _photoRepository.DidNotReceive().UpdateRangeAsync(Arg.Any<List<PhotoEntity>>(), default);
        }

        [Theory, AutoMoqData]
        public void UpdateAvatarAsync_InvalidPhotoId_ShouldThrowException(
            UserEntity userEntity
        )
        {
            // Arrange
            var userId = Guid.NewGuid();
            var photoId = Guid.NewGuid();

            userEntity.Id = userId;

            _userRepository.GetByIdAsync(userId, default).Returns(userEntity);
            _photoRepository.GetByIdAsync(photoId, userId, default).ReturnsNull();

            // Act
            var action = async () => await _photoService.UpdateAvatarAsync(userId, photoId, default);

            // Assert
            action.ShouldThrow<NotFoundException>();
            _photoRepository.DidNotReceive().UpdateRangeAsync(Arg.Any<List<PhotoEntity>>(), default);
        }
    }
}
