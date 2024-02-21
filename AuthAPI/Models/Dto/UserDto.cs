namespace AuthAPI.Models.Dto
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
    }

    public class UserDtoBuilder
    {
        private readonly UserDto _userDto;

        public UserDtoBuilder()
        {
            _userDto = new UserDto();
        }

        public UserDto FromUser(User user)
        {
            _userDto.Id = user.Id;
            _userDto.Name = user.Name;
            _userDto.Email = user.Email;
            _userDto.PhoneNumber = user.PhoneNumber;
            return _userDto;
        }
    }
}
