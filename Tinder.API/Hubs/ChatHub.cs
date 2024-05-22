using Microsoft.AspNetCore.SignalR;
using Tinder.API.Hubs.Connection;
using Tinder.API.Hubs.Interfaces;
using Tinder.BLL.Interfaces;

namespace Tinder.API.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {

        private readonly IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task JoinChat(ChatConnection chatConnection)
        {
            var chat = await _chatService.GetByIdAsync(chatConnection.ChatId, cancellationToken: default);
        }
    }
}
