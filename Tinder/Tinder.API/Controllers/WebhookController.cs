using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Tinder.BLL.Interfaces;
using System.Text.Json.Nodes;
using Tinder.API.Models;

namespace Tinder.API.Controllers
{
    [Route("api/webhook")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public WebhookController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<UserDto> Webhook(JsonObject userJson, CancellationToken cancellationToken)
        {
            var user = await _userService.CreateUserFromJson(userJson, cancellationToken);
            return _mapper.Map<UserDto>(user);
        }
    }
}
