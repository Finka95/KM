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

        public async Task JoinChat(ChatConnection connection)
        {
            var chat = await _chatService.GetByIdAsync(connection.ChatId, cancellationToken: default) ?? throw new NotFoundException("Chat is not found");
            var user = await _userService.GetByIdAsync(connection.UserId, cancellationToken: default) ?? throw new NotFoundException("User is not found");

            var stringConnection = JsonSerializer.Serialize(connection);
            await _cache.SetStringAsync(Context.ConnectionId, stringConnection);

            var roomName = connection.ChatId.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).ReceiveMessage("MyApp", $"{user.FirstName} is in the chat waiting for your message");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {

            var stringConnection = await _cache.GetAsync(Context.ConnectionId);
            var connection = JsonSerializer.Deserialize<ChatConnection>(stringConnection);

            var user = await _userService.GetByIdAsync(connection.UserId, cancellationToken: default);

            if (connection is not null)
            {
                await _cache.RemoveAsync(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatId.ToString());

                await Clients
                    .Group(connection.ChatId.ToString())
                    .ReceiveMessage("MyApp", $"User: {user.FirstName} is not online anymore");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
