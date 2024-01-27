using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Data;
using UsersAPI.Lib;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _responseDto;

        public UsersController(AppDbContext db)
        {
            _db = db;
            _responseDto = new ResponseDto();
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            _responseDto.Result = await _db.Users.ToListAsync();
            return Ok(_responseDto);
        }

        [HttpGet]
        [Route("{userId:int}")]
        public async Task<ActionResult> Get(int userId)
        {
            _responseDto.Result = await _db.Users.FirstOrDefaultAsync(user => user.UserId == userId);
            if (_responseDto.Result == null)
            {
                _responseDto.ErrorMessage = $"Incorrest userId: {userId}";
            }
            return Ok(_responseDto);
        }
    }
}
