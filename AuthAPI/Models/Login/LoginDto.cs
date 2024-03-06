using AuthAPI.Models.Dto;

namespace AuthAPI.Models.Login
{
    public class LoginDto
    {
        public required UserDto User { get; set; }
        public required string Token { get; set; }
    }
}
