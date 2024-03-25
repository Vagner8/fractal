using Microsoft.AspNetCore.Mvc;
using AuthAPI.Models.Response;
using AuthAPI.Models.Auth;
using Microsoft.AspNetCore.Identity;
using AuthAPI.Models.User;
using AuthAPI.Services.TokenGeneratorService;
using AuthAPI.Services.RoleService;

namespace AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(
        UserManager<User> userManager,
        IRoleService roleService,
        ITokenGeneratorService tokenGeneratorService) : ControllerBase
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly IRoleService _roleService = roleService;
        private readonly ITokenGeneratorService _tokenGeneratorService = tokenGeneratorService;

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var newUser = UserBuilder.ToUser(registerDto);
                var identityResult = await _userManager.CreateAsync(newUser, registerDto.Password);
                if (!identityResult.Succeeded) return BadRequest(ResponseBuilder.SetError(identityResult.Errors.FirstOrDefault()?.Description));
                var user = await _userManager.FindByEmailAsync(registerDto.Email);
                if (user == null) return NotFound(ResponseBuilder.SetError($"Register email: {registerDto.Email}"));
                await _roleService.AssignRole(user, registerDto.Role);
                return Ok(ResponseBuilder.SetData(user.UserName));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.SetError(ex.Message));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginRequestResponse)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(loginRequestResponse.Email);
                if (user == null) return NotFound(ResponseBuilder.SetError($"Unvalid email"));
                var isValid = await _userManager.CheckPasswordAsync(user, loginRequestResponse.Password);
                if (!isValid) return BadRequest(ResponseBuilder.SetError("Unvalid password"));
                var roles = await _userManager.GetRolesAsync(user);
                var token = _tokenGeneratorService.GenerateToken(user, roles);
                var userdto = UserBuilder.ToUserDto(user, token);
                return Ok(ResponseBuilder.SetData(userdto));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseBuilder.SetError(ex.Message));
            }
        }
    }
}
