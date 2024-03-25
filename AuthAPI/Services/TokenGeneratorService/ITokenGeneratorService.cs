using AuthAPI.Models.User;

namespace AuthAPI.Services.TokenGeneratorService
{
    public interface ITokenGeneratorService
    {
        public string GenerateToken(User user, IEnumerable<string> roles);
    }
}
