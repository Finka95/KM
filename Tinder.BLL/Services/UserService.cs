using AutoMapper;
using Tinder.BLL.Interfaces;
using Tinder.BLL.Models;
using Tinder.DAL.Entities;
using Tinder.DAL.Interfaces;

namespace Tinder.BLL.Services
{
    public class UserService : GenericService<User, UserEntity>, IUserService
    {

        public UserService(IUserRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
           
        }

    }
}
