namespace AuthAPI.Models.Dto
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
    }

    public class UserDtoMap
    {
        public static UserDto ToUserDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
            };
        }

        public static UserDto[] ToUsersDto(User[] users)
        {
            return users.Select(user => ToUserDto(user)).ToArray();
        }
    }
}
