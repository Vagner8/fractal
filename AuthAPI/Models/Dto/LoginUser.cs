namespace AuthAPI.Models.Dto
{
    public class LoginDto
    {
        public required UserDto User { get; set; }
        public required string Token { get; set; }
    }
}
