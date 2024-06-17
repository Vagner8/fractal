using MatrixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Matrix> Matrixes { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Control> Controls { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<Unit>()
        .HasMany(u => u.Controls)
        .WithOne(c => c.Unit)
        .HasForeignKey(c => c.UnitId)
        .OnDelete(DeleteBehavior.Cascade);

      builder.Entity<Unit>()
        .HasMany(u => u.Units)
        .WithOne(c => c.UnitInstance)
        .HasForeignKey(c => c.UnitId)
        .OnDelete(DeleteBehavior.Restrict);

      Seeding(builder);
    }

    private static void Seeding(ModelBuilder builder)
    {
      var matrix = Guid.NewGuid();

      var users = Guid.NewGuid();
      var products = Guid.NewGuid();

      var user_1 = Guid.NewGuid();
      var user_2 = Guid.NewGuid();

      var product_1 = Guid.NewGuid();
      var product_2 = Guid.NewGuid();

      builder.Entity<Matrix>().HasData(new Matrix { Id = matrix });

      builder.Entity<Unit>().HasData(
        new Unit { Id = users, MatrixId = matrix },
        new Unit { Id = products, MatrixId = matrix },

        new Unit { Id = user_1, UnitId = users },
        new Unit { Id = user_2, UnitId = users },

        new Unit { Id = product_1, UnitId = products },
        new Unit { Id = product_2, UnitId = products });

      builder.Entity<Control>().HasData(
        new Control { Id = Guid.NewGuid(), Data = "group", Indicator = Indicator.Icon, MatrixId = matrix },
        new Control { Id = Guid.NewGuid(), Data = "Users:Products", Indicator = Indicator.Sort, MatrixId = matrix },

        new Control { Id = Guid.NewGuid(), Data = "Users", Indicator = Indicator.Group, UnitId = users },
        new Control { Id = Guid.NewGuid(), Data = "group", Indicator = Indicator.Icon, UnitId = users },
        new Control { Id = Guid.NewGuid(), Data = "Email:Name", Indicator = Indicator.Sort, UnitId = users },

        new Control { Id = Guid.NewGuid(), Data = "Products", Indicator = Indicator.Group, UnitId = products },
        new Control { Id = Guid.NewGuid(), Data = "group", Indicator = Indicator.Icon, UnitId = products },
        new Control { Id = Guid.NewGuid(), Data = "Price:Name", Indicator = Indicator.Sort, UnitId = products },

        new Control { Id = Guid.NewGuid(), Data = "Dima", Indicator = "Name", UnitId = user_1 },
        new Control { Id = Guid.NewGuid(), Data = "dima@mail.com", Indicator = "Email", UnitId = user_1 },
        new Control { Id = Guid.NewGuid(), Data = "Anna", Indicator = "Name", UnitId = user_2 },
        new Control { Id = Guid.NewGuid(), Data = "anna@mail.com", Indicator = "Email", UnitId = user_2 },

        new Control { Id = Guid.NewGuid(), Data = "Phone", Indicator = "Name", UnitId = product_1 },
        new Control { Id = Guid.NewGuid(), Data = "1000", Indicator = "Price", UnitId = product_1 },
        new Control { Id = Guid.NewGuid(), Data = "Laptop", Indicator = "Name", UnitId = product_2 },
        new Control { Id = Guid.NewGuid(), Data = "5000", Indicator = "Price", UnitId = product_2 });
    }
  }
}
