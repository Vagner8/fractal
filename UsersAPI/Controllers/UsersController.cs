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
        public async Task<ActionResult> Get()
        {
            return Ok(new ResponseDto
            {
                Result = _mapper.Map<List<UserDto>>(await _db.Users.ToListAsync())
            });
        }

        [HttpGet]
        [Route("{userId:Guid}")]
        public async Task<ActionResult> Get(Guid userId)
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
                Result = _mapper.Map<UserDto>(user)
            });
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDto userDto)
        {
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
        [Route("{userId:Guid}")]
        public ActionResult Delete(Guid userId)
        {
            User user = _db.Users.First(user => user.UserId == userId);
            _db.Users.Remove(user);
            _db.SaveChanges();
            return Ok(new ResponseDto
            {
                Result = _mapper.Map<UserDto>(user)
            });
        }
    }
}
