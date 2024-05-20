using AutoMapper;
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
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);

            if (model.IsAvatar && user.Photos.Any(p => p.IsAvatar))
            {
                foreach (var userPhoto in user.Photos)
                {
                    if (userPhoto.IsAvatar)
                        userPhoto.IsAvatar = false;
                }

                var photoEntities = _mapper.Map<List<PhotoEntity>>(user.Photos);
                await _photoRepository.UpdateRangeAsync(photoEntities, cancellationToken);
            }

            var photoEntity = _mapper.Map<PhotoEntity>(model);
            photoEntity.UserId = userId;
            var photo = await _photoRepository.CreateAsync(photoEntity, cancellationToken);
            return _mapper.Map<Photo>(photo);
        }

        public async Task<Photo> GetByIdAsync(Guid userId, Guid id, CancellationToken cancellationToken)
        {
            var photoEntity = await _photoRepository.GetByIdAsync(id, userId, cancellationToken);
            return _mapper.Map<Photo>(photoEntity);
        }

        public async Task<Photo> UpdateAsync(Guid userId, Guid id, Photo model, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
            model.Id = id;
            if (model.IsAvatar && user.Photos.Any(p => p.IsAvatar))
            {
                foreach (var userPhoto in user.Photos)
                {
                    if (userPhoto.IsAvatar)
                        userPhoto.IsAvatar = false;
                }

                var photoEntities = _mapper.Map<List<PhotoEntity>>(user.Photos);
                await _photoRepository.UpdateRangeAsync(photoEntities, cancellationToken);
            }

            var photoEntity = _mapper.Map<PhotoEntity>(model);
            var photo = await _photoRepository.UpdateAsync(photoEntity, cancellationToken);
            photo.UserId = userId;
            return _mapper.Map<Photo>(photo);
        }
    }
}
