using AuthAPI.Data;
using AuthAPI.Models;
using AuthAPI.Models.Dto;
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
            var newUser = new UserBuilder().FromRegistrationDto(registrationDto);
            var identityResult = await _userManager.CreateAsync(newUser, registrationDto.Password);
            if (!identityResult.Succeeded)
            {
                var errorMessage = identityResult.Errors.FirstOrDefault()?.Description;
                return new ResponseDtoBuilder().SetError(errorMessage).Get();
            }
            var user = await GetUserByEmail(registrationDto);
            UserDto userDto = UserDtoMap.ToUserDto(user);
            return new ResponseDtoBuilder().SetData(userDto).Get();
        }

        public async Task<ResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(user => user.Email == loginRequestDto.Email);
            if (user == null) return new ResponseDtoBuilder().SetError("User not found").Get();
            var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!isValid) return new ResponseDtoBuilder().SetError("Unvalid password").Get();
            var token = _tokenGeneratorService.GenerateToken(user);
            return new ResponseDtoBuilder().SetToken(token).SetData(UserDtoMap.ToUserDto(user)).Get();
        }

        public async Task<ResponseDto> AssignRole(RegistrationDto registrationDto)
        {
            var user = await GetUserByEmail(registrationDto);
            var isRoleExist = await _roleManager.RoleExistsAsync(registrationDto.Role);
            if (!isRoleExist)
            {
                await _roleManager.CreateAsync(new IdentityRole(registrationDto.Role));
            }
            await _userManager.AddToRoleAsync(user, registrationDto.Role);
            return new ResponseDtoBuilder().SetData(user).Get();
        }

        private async Task<User> GetUserByEmail(RegistrationDto registrationDto)
        {
            return await _appDbContext.Users.FirstAsync(user => user.UserName == registrationDto.Email);
        }
    }
}
