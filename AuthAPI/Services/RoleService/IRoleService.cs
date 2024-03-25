using AuthAPI.Models.Auth;
using AuthAPI.Models.Response;
using AuthAPI.Models.User;

namespace AuthAPI.Services.RoleService
{
    public interface IRoleService
    {
        public Task<ResponseDto> AssignRole(User user, string Role);
    }
}
