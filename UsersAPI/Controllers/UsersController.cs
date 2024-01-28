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

        public UsersController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(new ResponseDto
            {
                Result = await _db.Users.ToListAsync()
            });
        }

        [HttpGet]
        [Route("{userId:int}")]
        public async Task<ActionResult> Get(int userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.UserId == userId);
            if (user == null)
            {
                return NotFound(new ResponseDto
                {
                    ErrorMessage = $"Incorrest userId: {userId}",
                    Status = StatusCodes.Status404NotFound
                });
            }
            return Ok(new ResponseDto
            {
                Result = user
            });
        }
    }
}
