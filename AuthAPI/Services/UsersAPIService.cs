using AuthAPI.Data;
using AuthAPI.Models;
using AuthAPI.Models.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Services
{
    public class UsersAPIService : IUsersAPIService
    {
        //private readonly AppDbContext _db;
        private readonly UserManager<User> _manager;

        public UsersAPIService(UserManager<User> manager)
        {
            //this._db = db;
            this._manager = manager;
        }

        public async Task<ResponseDto> GetUsers()
        {
            return new ResponseDtoBuilder().SetData(await _manager.Users.ToListAsync()).Get();
        }
    }
}
