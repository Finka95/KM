using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Tinder.API.Hubs.Connection;
using Tinder.API.Hubs.Interfaces;
using Tinder.BLL.Exceptions;
using Tinder.BLL.Interfaces;

namespace Tinder.API.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {

        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IDistributedCache _cache;

        public ChatHub(IChatService chatService, IUserService userService, IDistributedCache cache)
        {
            _chatService = chatService;
            _userService = userService;
            _cache = cache;
        }

        public async Task JoinChat(ChatConnection chatConnection)
        {
            var chat = await _chatService.GetByIdAsync(chatConnection.ChatId, cancellationToken: default) ?? throw new NotFoundException("Chat is not found");
            var user = await _userService.GetByIdAsync(chatConnection.UserId, cancellationToken: default);

            var stringConnection = JsonSerializer.Serialize(chatConnection);
            await _cache.SetStringAsync(Context.ConnectionId, stringConnection);

            var roomName = chatConnection.ChatId.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).ReceiveMessage("MyApp", $"{user.FirstName} is in the chat waiting for your message");
        }
    }
}
