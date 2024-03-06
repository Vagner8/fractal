using AuthAPI.Models.Dto;
using AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly UserManager<User> _manager;

        public UserAPIController(UserManager<User> manager)
        {
            this._manager = manager;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var users = await _manager.Users.ToArrayAsync();
                return Ok(new ResponseDtoBuilder().SetData(UserDtoMap.ToUsersDto(users)).Get());
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseDtoBuilder().SetError(ex.Message).Get());
            }
        }
    }
}
