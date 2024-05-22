using Microsoft.AspNetCore.SignalR;
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

        public ChatHub(IChatService chatService, IUserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }

        public async Task JoinChat(ChatConnection chatConnection)
        {
            var chat = await _chatService.GetByIdAsync(chatConnection.ChatId, cancellationToken: default) ?? throw new NotFoundException("Chat is not found");
            var user = await _userService.GetByIdAsync(chatConnection.UserId, cancellationToken: default);

            var roomName = chatConnection.ChatId.ToString();
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).ReceiveMessage("MyApp", $"{user.FirstName} is in the chat waiting for your message");
        }
    }
}
