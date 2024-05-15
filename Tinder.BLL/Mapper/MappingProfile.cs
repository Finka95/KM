using AutoMapper;
using Tinder.BLL.Models;
using Tinder.DAL.Entities;

namespace Tinder.BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserEntity, User>().ReverseMap();
            CreateMap<ChatEntity, Chat>().ReverseMap();
            CreateMap<MessageEntity, Message>().ReverseMap();
            CreateMap<PhotoEntity, Photo>().ReverseMap();
            CreateMap<LikeEntity, Like>().ReverseMap();
        }
    }
}
