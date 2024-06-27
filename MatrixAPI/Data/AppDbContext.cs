using MatrixAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Data
{
  public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
  {
    public DbSet<Unit> Units { get; set; }
    public DbSet<Control> Controls { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);

      builder.Entity<Unit>()
        .HasMany(u => u.Units)
        .WithOne(u => u.Parent)
        .HasForeignKey(u => u.ParentId)
        .OnDelete(DeleteBehavior.Restrict);

      builder.Entity<Unit>()
        .HasMany(u => u.Controls)
        .WithOne(c => c.Parent)
        .HasForeignKey(c => c.ParentId)
        .OnDelete(DeleteBehavior.Cascade);

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

      builder.Entity<Unit>().HasData(
        new Unit { Id = matrix },
        new Unit { Id = users, ParentId = matrix },
        new Unit { Id = products, ParentId = matrix },

        new Unit { Id = user_1, ParentId = users },
        new Unit { Id = user_2, ParentId = users },

        new Unit { Id = product_1, ParentId = products },
        new Unit { Id = product_2, ParentId = products });

      builder.Entity<Control>().HasData(
        new Control { Id = Guid.NewGuid(), Data = "Vega", Indicator = Indicator.Unit, ParentId = matrix },
        new Control { Id = Guid.NewGuid(), Data = "Users:Products", Indicator = Indicator.Sort, ParentId = matrix },

        new Control { Id = Guid.NewGuid(), Data = "Users", Indicator = Indicator.Unit, ParentId = users },
        new Control { Id = Guid.NewGuid(), Data = "group", Indicator = Indicator.Icon, ParentId = users },
        new Control { Id = Guid.NewGuid(), Data = "Email:Name", Indicator = Indicator.Sort, ParentId = users },

        new Control { Id = Guid.NewGuid(), Data = "Products", Indicator = Indicator.Unit, ParentId = products },
        new Control { Id = Guid.NewGuid(), Data = "widgets", Indicator = Indicator.Icon, ParentId = products },
        new Control { Id = Guid.NewGuid(), Data = "Price:Name", Indicator = Indicator.Sort, ParentId = products },

        new Control { Id = Guid.NewGuid(), Data = "Dima", Indicator = Indicator.Name, ParentId = user_1 },
        new Control { Id = Guid.NewGuid(), Data = "dima@mail.com", Indicator = "Email", ParentId = user_1 },
        new Control { Id = Guid.NewGuid(), Data = "Anna", Indicator = Indicator.Name, ParentId = user_2 },
        new Control { Id = Guid.NewGuid(), Data = "anna@mail.com", Indicator = "Email", ParentId = user_2 },

        new Control { Id = Guid.NewGuid(), Data = "Phone", Indicator = Indicator.Name, ParentId = product_1 },
        new Control { Id = Guid.NewGuid(), Data = "1000", Indicator = "Price", ParentId = product_1 },
        new Control { Id = Guid.NewGuid(), Data = "Laptop", Indicator = Indicator.Name, ParentId = product_2 },
        new Control { Id = Guid.NewGuid(), Data = "5000", Indicator = "Price", ParentId = product_2 });
    }
  }
}
