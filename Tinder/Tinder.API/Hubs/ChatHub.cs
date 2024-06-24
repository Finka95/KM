using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using Tinder.API.DTO.CreateDto;
using Tinder.API.Hubs.Connection;
using Tinder.API.Hubs.Interfaces;
using Tinder.Bll.Exceptions;
using Tinder.BLL.Exceptions;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;

namespace Tinder.API.Hubs
{
    public class ChatHub : Hub<IChatHub>
    {
        private readonly IChatService _chatService;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IDistributedCache _cache;
        private readonly IMapper _mapper;

        public ChatHub(IChatService chatService, IUserService userService, IMessageService messageService, IDistributedCache cache, IMapper mapper)
        {
            _chatService = chatService;
            _userService = userService;
            _messageService = messageService;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task JoinChat(ChatConnection connection)
        {
            var chat = await _chatService.GetByIdAsync(connection.ChatId, cancellationToken: default) ?? throw new NotFoundException("Chat is not found");
            var user = await _userService.GetByIdAsync(connection.UserId, cancellationToken: default) ?? throw new NotFoundException("User is not found");
            
            if (chat.Users.All(u => u.Id != user.Id)) throw new BadRequestException("User is not allowed to be in the chat");

            var stringConnection = JsonSerializer.Serialize(connection);
            await _cache.SetStringAsync(Context.ConnectionId, stringConnection);
            
            var groupName = connection.ChatId.ToString();

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).ReceiveMessage("MyApp", $"{user.FirstName} is in the chat waiting for your message");
        }

        public async Task ShowPreviousMessages(ChatConnection connection)
        {
            var chat = await _chatService.GetByIdWithUsersAndMessagesAsync(connection.ChatId, cancellationToken: default) ?? throw new NotFoundException("Chat is not found");
            var sortedMessages = chat.Messages.OrderBy(m => m.Date).ToList();

            foreach (var message in sortedMessages)
            {
                await Clients
                    .Caller
                    .ReceiveMessage(message.User.FirstName, message.Text);
            }
        }

        public async Task SendMessage(string message)
        {
            var stringConnection = await _cache.GetAsync(Context.ConnectionId);
            var connection = JsonSerializer.Deserialize<ChatConnection>(stringConnection);

            if (connection is not null)
            {
                var sender = await _userService.GetByIdAsync(connection.UserId, cancellationToken: default);
                var chat = await _chatService.GetByIdAsync(connection.ChatId, cancellationToken: default);

                var messageDto = new CreateMessageDto()
                {
                    ChatId = chat.Id,
                    Chat = chat,
                    SenderId = sender.Id,
                    User = sender,
                    Date = DateTime.Now,
                    Text = message,
                };

                var messageToCreate = _mapper.Map<Message>(messageDto);

                await _messageService.CreateAsync(messageToCreate, cancellationToken: default);

                await Clients
                    .Group(connection.ChatId.ToString())
                    .ReceiveMessage(sender.FirstName, message);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {

            var stringConnection = await _cache.GetAsync(Context.ConnectionId);
            var connection = JsonSerializer.Deserialize<ChatConnection>(stringConnection);

            if (connection is not null)
            {
                var user = await _userService.GetByIdAsync(connection.UserId, cancellationToken: default);
                await _cache.RemoveAsync(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatId.ToString());
              
                await Clients
                   .OthersInGroup(connection.ChatId.ToString())
                   .ReceiveMessage("MyApp", $"{user.FirstName} is not in the chat");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
