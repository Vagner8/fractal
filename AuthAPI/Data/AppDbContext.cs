using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Models.User;

namespace UsersAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}