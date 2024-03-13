using UsersAPI.Models.Auth;

namespace UsersAPI.Models.User
{
    public class UserBuilder
    {
        public static User ToUser(RegisterDto registrationDto)
        {
            return new User
            {
                UserName = registrationDto.Email,
                Email = registrationDto.Email,
            };
        }

        public static UserDto ToUserDto(User user, string token)
        {
            return new UserDto
            {
                Name = user.UserName,
                Token = token,
            };
        }
    }
}
