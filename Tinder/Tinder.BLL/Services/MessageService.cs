using AutoMapper;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.BLL.Services
{
    public class MessageService : GenericService<Message, MessageEntity>, IMessageService
    {

        public MessageService(IMessageRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            
        }

    }
}
