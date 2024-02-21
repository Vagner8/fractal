using AuthAPI.Data;
using AuthAPI.Models;
using AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using AuthAPI.Services;

namespace AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public AuthAPIController(
            AppDbContext appDbContext,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenGeneratorService tokenGeneratorService)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenGeneratorService = tokenGeneratorService;
        }

        [HttpPost("regester")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            try
            {
                var newUser = new UserBuilder().FromRegistrationDto(registrationDto);
                var identityResult = await _userManager.CreateAsync(newUser, registrationDto.Password);
                if (!identityResult.Succeeded) {
                    var errorMasage = identityResult.Errors.FirstOrDefault()?.Description;
                    return BadRequest(new ResponseDtoBuilder().SetError(errorMasage).Get());
                }
                var user = await _appDbContext.Users.FirstAsync(user => user.UserName == registrationDto.Email);
                UserDto userDto = new UserDtoBuilder().FromUser(user);
                return Ok(new ResponseDtoBuilder().SetResult(userDto).Get());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDtoBuilder().SetError(ex.Message).Get());
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.UserName == loginRequestDto.UserName);
            if (user == null) return NotFound(new ResponseDtoBuilder().SetError("User not found").Get());
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!isValid) return BadRequest(new ResponseDtoBuilder().SetError("Unvalid password").Get());
            var token = _tokenGeneratorService.GenerateToken(user);
            return Ok(new ResponseDtoBuilder().SetToken(token).SetResult(new UserDtoBuilder().FromUser(user)).Get());
        }
    }
}
