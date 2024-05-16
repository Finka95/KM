using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tinder.API.DTO.CreateDto;
using Tinder.API.Models;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;

namespace Tinder.API.Controllers
{
    [Route("api/likes")]
    [ApiController]
    public class LikeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService, IMapper mapper)
        {
            _mapper = mapper;
            _likeService = likeService;
        }

        [HttpGet("{id}")]
        public async Task<LikeDto> GetById(Guid id, CancellationToken cancellationToken)
        {
            var model = await _likeService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<LikeDto>(model);
        }

        [HttpGet]
        public async Task<List<LikeDto>> GetAll(CancellationToken cancellationToken)
        {
            var likeModels = await _likeService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<LikeDto>>(likeModels);
        }

        [HttpPost]
        public async Task<LikeDto> Create(CreateLikeDto createLikeDto, CancellationToken cancellationToken)
        {
            var likeToCreate = _mapper.Map<Like>(createLikeDto);
            var model = await _likeService.CreateAsync(likeToCreate, cancellationToken);
            return _mapper.Map<LikeDto>(model);
        }

        [HttpDelete("{id}")]
        public async Task<LikeDto> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            var model = await _likeService.DeleteAsync(id, cancellationToken);
            return _mapper.Map<LikeDto>(model);
        }
    }
}
