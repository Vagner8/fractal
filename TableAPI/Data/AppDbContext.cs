using MatrixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Data
{
  public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
  {
    public DbSet<Matrix> Matrices { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Control> Controls { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Matrix>()
        .HasMany(m => m.Units)
        .WithOne(u => u.Matrix)
        .OnDelete(DeleteBehavior.Cascade);

      builder.Entity<Unit>()
        .HasMany(u => u.Controls)
        .WithOne(c => c.Unit)
        .OnDelete(DeleteBehavior.Cascade);

      base.OnModelCreating(builder);
    }
  }
}
