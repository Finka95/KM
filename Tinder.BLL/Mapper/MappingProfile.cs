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
        }
    }
}
