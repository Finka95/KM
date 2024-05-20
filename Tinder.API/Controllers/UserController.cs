using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tinder.API.DTO.CreateDto;
using Tinder.API.DTO.UpdateDto;
using Tinder.API.Models;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;

namespace Tinder.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetById(Guid id, CancellationToken cancellationToken)
        {
            var model = await _userService.GetByIdAsync(id, cancellationToken);
            return _mapper.Map<UserDto>(model);
        }

        [HttpGet]

        public async Task<List<UserDto>> GetAll(CancellationToken cancellationToken)
        {
            var userModels = await _userService.GetAllAsync(cancellationToken);
            return _mapper.Map<List<UserDto>>(userModels.ToList());
        }

        [HttpPut("{id}")]
        public async Task<UserDto> UpdateById(Guid id, UpdateUserDto dto, CancellationToken cancellationToken)
        {
            var userToUpdate = _mapper.Map<User>(dto);
            var user = await _userService.UpdateAsync(id, userToUpdate, cancellationToken);
            return _mapper.Map<UserDto>(user);
        }

        [HttpPost]
        public async Task<UserDto> Create(CreateUserDto dto, CancellationToken cancellationToken)
        {
            var userToCreate = _mapper.Map<User>(dto);
            var model = await _userService.CreateAsync(userToCreate, cancellationToken);
            return _mapper.Map<UserDto>(model);
        }

        [HttpDelete("{id}")]
        public async Task<UserDto> DeleteById(Guid id, CancellationToken cancellationToken)
        {
            var model = await _userService.DeleteAsync(id, cancellationToken);
            return _mapper.Map<UserDto>(model);
        }
    }
}
