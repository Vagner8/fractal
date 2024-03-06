using AuthAPI.Models;
using AuthAPI.Models.Login;
using AuthAPI.Models.ResponseDto;

namespace AuthAPI.Services
{
    public interface IAuthAPIService
    {
        Task<ResponseDto> Register(RegistrationDto registrationDto);
        Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<ResponseDto> AssignRole(RegistrationDto registrationDto);
    }
}
