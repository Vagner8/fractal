using FractalAPI.Models;
using FractalAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace FractalAPI.Data
{
  public class AppDbContext(
    DbContextOptions<AppDbContext> options) : DbContext(options)
  {
    public DbSet<Fractal> Fractals { get; set; }
    public DbSet<Control> Controls { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      builder.Entity<Fractal>()
        .HasMany(f => f.Fractals)
        .WithOne(f => f.Parent)
        .HasForeignKey(f => f.ParentId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.Entity<Fractal>()
        .HasMany(f => f.Controls)
        .WithOne(c => c.Parent)
        .HasForeignKey(c => c.ParentId)
        .OnDelete(DeleteBehavior.Cascade);

      var (fractals, controls) = new SeedingService();

      builder.Entity<Fractal>().HasData(fractals);
      builder.Entity<Control>().HasData(controls);
    }
  }
}