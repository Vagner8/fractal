using UsersAPI.Models.User;

namespace UsersAPI.Services.TokenGeneratorService
{
    public interface ITokenGeneratorService
    {
        public string GenerateToken(User user, IEnumerable<string> roles);
    }
}
