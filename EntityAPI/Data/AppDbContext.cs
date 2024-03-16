using EntityAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Field> Fields { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
