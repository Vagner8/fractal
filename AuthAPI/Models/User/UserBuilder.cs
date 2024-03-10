using AuthAPI.Models.Dto;

namespace AuthAPI.Models.User
{
    public class UserBuilder
    {
        public static User ToUser(RegistrationDto registrationDto)
        {
            return new User
            {
                Name = registrationDto.Name,
                Surname = registrationDto.Surname,
                UserName = registrationDto.Email,
                PhoneNumber = registrationDto.Phone,
                Email = registrationDto.Email,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                NormalizedEmail = registrationDto.Email.ToUpper(),
            };
        }

        public static UserDto ToUserDto(User user, string? token = null)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Created = user.Created,
                Updated = user.Updated,
                Token = token,
            };
        }
    }
}
