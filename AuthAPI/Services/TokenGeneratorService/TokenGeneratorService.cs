using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthAPI.Models.User;
using AuthAPI.Models.Auth;

namespace AuthAPI.Services.TokenGeneratorService
{
    public class TokenGeneratorService(IOptions<JwtOptions> jwtOptions) : ITokenGeneratorService
    {
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        public string GenerateToken(User user, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);
            var claimList = GetClaimList(user);
            AddRolesToClaimList(claimList, roles);
            var tokenDescriptor = GetTokenDescriptor(claimList, key);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private void AddRolesToClaimList(List<Claim> claimList, IEnumerable<string> roles)
        {
            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        }

        private List<Claim> GetClaimList(User user)
        {
            return
            [
                new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty)
            ];
        }

        private SecurityTokenDescriptor GetTokenDescriptor(List<Claim> claimList, byte[] key)
        {
            return new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }
    }
}
