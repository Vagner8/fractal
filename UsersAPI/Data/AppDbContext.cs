using Microsoft.EntityFrameworkCore;
using UsersAPI.Models.User;

namespace UsersAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = Guid.NewGuid(),
                Login = "Vagner",
                FirstName = "Dmitry",
                LastName = "Vagner",
                Email = "myevropa1@gmail.com",
                Phone = "+420776544634",
                Password = "123456",
                Updated = DateTime.Now,
                Created = DateTime.Now
            });

            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = Guid.NewGuid(),
                Login = "Vagner",
                FirstName = "Dmitry",
                LastName = "Vagner",
                Email = "myevropa1@gmail.com",
                Phone = "+420776544634",
                Password = "123456",
                Updated = DateTime.Now,
                Created = DateTime.Now
            });
        }
    }
}
