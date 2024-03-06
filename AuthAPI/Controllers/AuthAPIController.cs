using Microsoft.AspNetCore.Mvc;
using AuthAPI.Services;
using AuthAPI.Models.Login;
using AuthAPI.Models;
using AuthAPI.Models.ResponseDto;

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

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegistrationDto registrationDto)
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
        public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
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

        [HttpPost("assignRole")]
        public async Task<ActionResult> AssignRole([FromBody] RegistrationDto registrationDto)
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
