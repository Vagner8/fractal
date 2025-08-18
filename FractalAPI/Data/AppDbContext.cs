using FractalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FractalAPI.Data
{
  public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
  {
    public DbSet<Fractal> Fractals { get; set; }
    public DbSet<Control> Controls { get; set; }

    public override int SaveChanges()
    {
      UpdateTimestamps();
      return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
      UpdateTimestamps();
      return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateTimestamps()
    {
      foreach (var entry in ChangeTracker.Entries())
      {
        DateTime now = DateTime.UtcNow;
        if (entry.Entity is Base entity)
        {
          if (entry.State == EntityState.Added)
          {
            entity.CreatedAt = now;
            entity.UpdatedAt = now;
          }

          if (entry.State == EntityState.Unchanged)
          {
            entity.UpdatedAt = now;
          }
        }
      }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<Fractal>()
        .HasMany(f => f.Children)
        .WithOne()
        .HasForeignKey(f => f.ParentCursor);

      builder.Entity<Fractal>()
        .HasMany(f => f.Controls)
        .WithOne()
        .HasForeignKey(c => c.ControlParentCursor);

      builder.Entity<Fractal>()
        .HasMany(f => f.ChildrenControls)
        .WithOne()
        .HasForeignKey(c => c.ChildControlParentCursor);

      var (fractals, controls, childrenControls) = new Seeding();

      builder.Entity<Fractal>().HasData(fractals);
      builder.Entity<Control>().HasData(controls);
      builder.Entity<Control>().HasData(childrenControls);
    }
  }
}