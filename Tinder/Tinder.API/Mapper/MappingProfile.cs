using AutoMapper;
using Tinder.API.DTO.CreateDto;
using Tinder.API.DTO.UpdateDto;
using Tinder.API.Models;
using Tinder.BLL.Models;

namespace Tinder.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<Photo, PhotoDto>();
            CreateMap<CreatePhotoDto, Photo>();
            CreateMap<Message, MessageDto>();
            CreateMap<CreateMessageDto, Message>();
            CreateMap<Like, LikeDto>();
            CreateMap<CreateLikeDto, Like>();   
            CreateMap<Chat, ChatDto>();
        }
    }
}
