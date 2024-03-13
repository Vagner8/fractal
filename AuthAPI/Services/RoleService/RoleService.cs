using Microsoft.AspNetCore.Identity;
using UsersAPI.Models.Response;
using UsersAPI.Models.User;

namespace UsersAPI.Services.RoleService
{
    public class RoleService(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager) : IRoleService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;

        public async Task<ResponseDto> AssignRole(User user, string Role)
        {
            var isRoleExist = await _roleManager.RoleExistsAsync(Role);
            if (!isRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(Role));
            }
            await _userManager.AddToRoleAsync(user, Role);
            return ResponseBuilder.SetData(user);
        }
    }
}
