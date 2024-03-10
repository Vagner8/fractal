using AuthAPI.Data;
using AuthAPI.Models;
using AuthAPI.Models.Login;
using AuthAPI.Models.ResponseDto;
using AuthAPI.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Services
{
    public class AuthAPIService : IAuthAPIService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenGeneratorService _tokenGeneratorService;

        public AuthAPIService(
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

        public async Task<ResponseDto> Register(RegistrationDto registrationDto)
        {
            var newUser = UserBuilder.ToUser(registrationDto);
            var identityResult = await _userManager.CreateAsync(newUser, registrationDto.Password);
            if (!identityResult.Succeeded)
            {
                var errorMessage = identityResult.Errors.FirstOrDefault()?.Description;
                return new ResponseDtoBuilder().SetError(errorMessage).Build();
            }
            var user = await GetUserByEmail(registrationDto.Email);
            var userDto = UserBuilder.ToUserDto(user);
            await AssignRole(registrationDto);
            return new ResponseDtoBuilder().SetData(userDto).Build();
        }

        public async Task<ResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await GetUserByEmail(loginRequestDto.Email);
            if (user == null) return new ResponseDtoBuilder().SetError("User not found").Build();
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!isValid) return new ResponseDtoBuilder().SetError("Unvalid password").Build();
            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenGeneratorService.GenerateToken(user, roles);
            return new ResponseDtoBuilder().SetData(UserBuilder.ToUserDto(user, token)).Build();
        }

        public async Task<ResponseDto> AssignRole(RegistrationDto registrationDto)
        {
            var user = await GetUserByEmail(registrationDto.Email);
            var isRoleExist = await _roleManager.RoleExistsAsync(registrationDto.Role);
            if (!isRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(registrationDto.Role));
            }
            await _userManager.AddToRoleAsync(user, registrationDto.Role);
            return new ResponseDtoBuilder().SetData(user).Build();
        }

        private async Task<User> GetUserByEmail(string Email)
        {
            return await _appDbContext.Users.FirstAsync(user => user.UserName == Email);
        }
    }
}
