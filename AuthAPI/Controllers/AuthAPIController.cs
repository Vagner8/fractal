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
        private readonly IAuthAPIService _authAPIService;

        public AuthAPIController(IAuthAPIService authAPIService)
        {
            _authAPIService = authAPIService;
        }

        [HttpPost("regester")]
        public async Task<IActionResult> Register([FromBody] RegistrationDto registrationDto)
        {
            try
            {
                var responseDto = await _authAPIService.Register(registrationDto);
                if (!responseDto.Success) return BadRequest(responseDto);
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDtoBuilder().SetError(ex.Message).Get());
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try
            {
                var responseDto = await _authAPIService.Login(loginRequestDto);
                if (!responseDto.Success) return BadRequest(responseDto);
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDtoBuilder().SetError(ex.Message).Get());
            }
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationDto registrationDto)
        {
            try
            {
                var responseDto = await _authAPIService.AssignRole(registrationDto);
                if (!responseDto.Success) return BadRequest(responseDto);
                return Ok(responseDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDtoBuilder().SetError(ex.Message).Get());
            }
        }
    }
}
