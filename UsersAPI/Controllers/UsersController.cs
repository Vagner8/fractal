using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Data;
using UsersAPI.Models;

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(AppDbContext db) : ControllerBase
    {
        private readonly AppDbContext _db = db;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            throw new InvalidOperationException("This operation is not valid.");
            //var response = new ResponseTest<List<User>>();
            //try
            //{
            //    return StatusCode(500, response);
            //    //response.Data = await _db.Users.ToListAsync();
            //    //return Ok(response);
            //}
            //catch (Exception ex)
            //{
            //    response.ErrorMessage = ex.Message;
            //    return StatusCode(500, response);
            //}
        }
    }
}
