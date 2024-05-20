using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tinder.API.DTO.CreateDto;
using Tinder.API.DTO.UpdateDto;
using Tinder.API.Models;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;

namespace Tinder.API.Controllers
{
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotoController
    {
        private readonly IPhotoService _photoService;
        private readonly IMapper _mapper;
        public PhotoController(IPhotoService photoService, IMapper mapper)
        {
            _photoService = photoService;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<PhotoDto> GetByIdAsync(Guid userId, Guid id, CancellationToken cancellationToken)
        {
            var model = await _photoService.GetByIdAsync(userId, id, cancellationToken);
            return _mapper.Map<PhotoDto>(model);
        }

        [HttpGet]

        public async Task<List<PhotoDto>> GetAll(CancellationToken cancellationToken)
        {
            var photoModels = await _photoService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<PhotoDto>>(photoModels.ToList());
        }

        [HttpPut("{id}")]
        public async Task<PhotoDto> UpdateById(Guid userId, Guid id, UpdatePhotoDto dto, CancellationToken cancellationToken)
        {
            var photoToUpdate = _mapper.Map<Photo>(dto);
            var photo = await _photoService.UpdateAsync(userId, id, photoToUpdate, cancellationToken);
            return _mapper.Map<PhotoDto>(photo);
        }

        [HttpPost]
        public async Task<PhotoDto> Create(Guid userId, CreatePhotoDto dto, CancellationToken cancellationToken)
        {
            var photoToCreate = _mapper.Map<Photo>(dto);
            var model = await _photoService.CreateAsync(userId, photoToCreate, cancellationToken);
            return _mapper.Map<PhotoDto>(model);
        }

        [HttpDelete("{id}")]
        public async Task<PhotoDto> Delete(Guid userId, Guid id, CancellationToken cancellationToken)
        {
            var model = await _photoService.DeleteAsync(userId, id, cancellationToken);
            return _mapper.Map<PhotoDto>(model);
        }
    }
}
