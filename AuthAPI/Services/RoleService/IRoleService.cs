using UsersAPI.Models.Auth;
using UsersAPI.Models.Response;
using UsersAPI.Models.User;

namespace UsersAPI.Services.RoleService
{
    public interface IRoleService
    {
        public Task<ResponseDto> AssignRole(User user, string Role);
    }
}
