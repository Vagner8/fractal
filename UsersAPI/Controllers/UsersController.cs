using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Data;
using UsersAPI.Lib;
using UsersAPI.Models.User;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public UsersController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] Guid? userId)
        {
            if (userId.HasValue)
            {
                var user = await _db.Users.FirstOrDefaultAsync(user => user.UserId == userId);             
                return Ok(new ResponseDto
                {
                    Result = _mapper.Map<UserDto>(user)
                });
            }
            else
            {
                return Ok(new ResponseDto
                {
                    Result = _mapper.Map<List<UserDto>>(await _db.Users.ToListAsync())
                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                // Custom response for validation errors
                var responseDto = new ResponseDto
                {
                    Result = null,
                    Status = StatusCodes.Status400BadRequest,
                    ErrorMessage = "Invalid user data. Please check the provided information."
                };

                return BadRequest(responseDto);
            }
            userDto.Updated = DateTime.Now;
            userDto.Created = DateTime.Now;
            userDto.UserId = Guid.NewGuid();
            User user = _mapper.Map<User>(userDto);
            await _db.Users.AddAsync(user);
            _db.SaveChanges();
            return Ok(new ResponseDto
            {
                Result = userDto
            });
        }

        [HttpPut]
        public ActionResult Put([FromBody] UserDto userDto)
        {
            userDto.Updated = DateTime.Now;
            _db.Users.Update(_mapper.Map<User>(userDto));
            _db.SaveChanges();
            return Ok(new ResponseDto
            {
                Result = userDto
            });
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] Guid userId)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.UserId == userId);
            if (user == null)
            {
                return NotFound(new ResponseDto
                {
                    ErrorMessage = $"Not possible delete user by id: {userId}",
                    Status = StatusCodes.Status404NotFound
                });
            }
            _db.Users.Remove(user);
            _db.SaveChanges();
            return Ok(new ResponseDto
            {
                Result = _mapper.Map<UserDto>(user)
            });
        }
    }
}
