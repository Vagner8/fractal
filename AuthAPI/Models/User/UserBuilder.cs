using AuthAPI.Models;
using AuthAPI.Models.Dto;

namespace AuthAPI.Models.User
{
    public class UserBuilder
    {
        public static User ToUser(RegistrationDto registrationDto)
        {
            return new User
            {
                UserName = registrationDto.Email,
                Email = registrationDto.Email,
                NormalizedEmail = registrationDto.Email.ToUpper(),
                Name = registrationDto.Name,
                Phone = registrationDto.Phone,
                Created = DateTime.Now,
                Updated = DateTime.Now,
            };
        }

        public static UserDto ToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Phone = user.Phone,
                Updated = user.Updated,
                Created = user.Updated,
            };
        }

        public static UserDto[] ToUsersDto(User[] users)
        {
            return users.Select(user => ToUserDto(user)).ToArray();
        }
    }
}
