using AutoMapper;
using UsersAPI.Models.User;

namespace UsersAPI.Services
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            return new MapperConfiguration(config =>
            {
                config.CreateMap<UserDto, User>();
                config.CreateMap<User, UserDto>().ForMember(dest => dest.Password, opt => opt.Ignore());
            });
        }
    }
}
