using AutoMapper;
using greenlit.Dtos;
using greenlit.Entities;

namespace greenlit.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
