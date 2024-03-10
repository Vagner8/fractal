using AuthAPI.Models.User;

namespace AuthAPI.Services
{
    public interface ITokenGeneratorService
    {
        public string GenerateToken(User user, IEnumerable<string> roles);
    }
}
