using AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime Updated { get; set; }
        public DateTime Created { get; set; }
    }

    public class UserBuilder
    {
        private readonly User _user;

        public UserBuilder()
        {
            _user = new User();
        }

        public User FromRegistrationDto(RegistrationDto registrationDto)
        {
            _user.UserName = registrationDto.Email;
            _user.Email = registrationDto.Email;
            _user.NormalizedEmail = registrationDto.Email.ToUpper();
            _user.Name = registrationDto.Name;
            _user.Phone = registrationDto.Phone;
            _user.Created = DateTime.Now;
            _user.Updated = DateTime.Now;
            return _user;
        }
    }
}
