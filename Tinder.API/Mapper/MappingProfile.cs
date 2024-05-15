using AutoMapper;
using Tinder.API.DTO.CreateDto;
using Tinder.API.Models;
using Tinder.BLL.Models;

namespace Tinder.API.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
