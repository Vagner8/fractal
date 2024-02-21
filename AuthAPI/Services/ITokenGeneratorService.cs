using AuthAPI.Models;

namespace AuthAPI.Services
{
    public interface ITokenGeneratorService
    {
        public string GenerateToken(User user);
    }
}
