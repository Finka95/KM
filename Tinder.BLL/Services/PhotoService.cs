using AutoMapper;
using Tinder.BLL.Exceptions;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.BLL.Services
{
    public class PhotoService : GenericService<Photo, PhotoEntity>, IPhotoService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository repository, IMapper mapper, IUserRepository userRepository)
            : base(repository, mapper)
        {
            _userRepository = userRepository;
            _photoRepository = repository;
        }

        public async Task<Photo> CreateAsync(Guid userId, Photo model, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetByIdAsync(userId, cancellationToken) ?? throw new NotFoundException("User is not found");
            var user = _mapper.Map<User>(userEntity);
            if (model.IsAvatar && userEntity.Photos.Any(p => p.IsAvatar))
            {
                var userPhoto = user.Photos.FirstOrDefault(p => p.IsAvatar);
                userPhoto.IsAvatar = false;
                var userPhotoEntity = _mapper.Map<PhotoEntity>(userPhoto);
                await _photoRepository.UpdateAsync(userPhotoEntity, cancellationToken);
            }

            var photoEntity = _mapper.Map<PhotoEntity>(model);
            photoEntity.UserId = userId;
            var photo = await _photoRepository.CreateAsync(photoEntity, cancellationToken);
            return _mapper.Map<Photo>(photo);
        }

        public async Task<Photo> GetByIdAsync(Guid userId, Guid id, CancellationToken cancellationToken)
        {
            var photoEntity = await _photoRepository.GetByIdAsync(id, userId, cancellationToken) ?? throw new NotFoundException(" Photo is not found");
            return _mapper.Map<Photo>(photoEntity);
        }

        public async Task<Photo> DeleteAsync(Guid userId, Guid id, CancellationToken cancellationToken)
        {
            var entity = await _photoRepository.GetByIdAsync(id, userId, cancellationToken) ?? throw new NotFoundException("Photo is not found");
            await _photoRepository.DeleteAsync(entity, cancellationToken);
            return _mapper.Map<Photo>(entity);
        }

        public async Task<Photo> UpdateAvatarAsync(Guid userId, Guid id, CancellationToken cancellationToken)
        {
            var userEntity = await _userRepository.GetByIdAsync(userId, cancellationToken) ?? throw new NotFoundException("User is not found");
            var user = _mapper.Map<User>(userEntity);

            var photoEntity = await _photoRepository.GetByIdAsync(id, userId, cancellationToken) ?? throw new NotFoundException("Photo is not found");
            photoEntity.IsAvatar = true;
            
            var previousAvatar = user.Photos.FirstOrDefault(p => p.IsAvatar);
            if (previousAvatar is null)
            {
                return _mapper.Map<Photo>(photoEntity);
            }

            previousAvatar.IsAvatar = false;
            var avatarEntity = _mapper.Map<PhotoEntity>(photoEntity);
            var previousAvatarEntity = _mapper.Map<PhotoEntity>(previousAvatar);
            var entitiesToUpdate = new List<PhotoEntity> { avatarEntity, previousAvatarEntity };

            await _photoRepository.UpdateRangeAsync(entitiesToUpdate, cancellationToken);
            return _mapper.Map<Photo>(avatarEntity);
        }
    }
}
