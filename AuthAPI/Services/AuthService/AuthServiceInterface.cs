using AuthAPI.Models.Dto;

namespace AuthAPI.Services.AuthService
{
    public interface AuthServiceInterface
    {
        Task<UserDto> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
