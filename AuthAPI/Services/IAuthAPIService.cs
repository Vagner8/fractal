using AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Services
{
    public interface IAuthAPIService
    {
        Task<ResponseDto> Register(RegistrationDto registrationDto);
        Task<ResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<ResponseDto> AssignRole(RegistrationDto registrationDto);
    }
}
