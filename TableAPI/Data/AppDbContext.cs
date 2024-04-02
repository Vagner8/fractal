using MatrixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Matrix> Matrices { get; set; }
        public DbSet<Line> Lines { get; set; }
        public DbSet<Control> Controls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
