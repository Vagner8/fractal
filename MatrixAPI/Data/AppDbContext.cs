using MatrixAPI.Models;
using MatrixAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace MatrixAPI.Data
{
  public class AppDbContext(IControlService control, DbContextOptions<AppDbContext> options) : DbContext(options)
  {
    private readonly IControlService _control = control;
    public DbSet<Matrix> Matrixes { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Control> Controls { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Matrix>()
        .HasMany(m => m.Groups)
        .WithOne(g => g.Matrix)
        .OnDelete(DeleteBehavior.Cascade);

      builder.Entity<Group>()
        .HasMany(g => g.Units)
        .WithOne(u => u.Group)
        .OnDelete(DeleteBehavior.Cascade);

      builder.Entity<Unit>()
        .HasMany(u => u.Controls)
        .WithOne(c => c.Unit)
        .OnDelete(DeleteBehavior.Cascade);

      var matrixId = Guid.NewGuid();
      var groupId = Guid.NewGuid();

      builder.Entity<Matrix>().HasData(new Matrix
      {
        Id = matrixId,
      });

      builder.Entity<Group>().HasData(new Group
      {
        Id = groupId,
        MatrixId = matrixId,
      });

      builder.Entity<Control>().HasData(
        _control.MatrixControl(Indicator.Matrix, Act.Add, matrixId),
        _control.MatrixControl(Indicator.Act, Act.Add, matrixId),
        _control.MatrixControl(Indicator.Icon, string.Empty, matrixId),
        _control.MatrixControl(Indicator.Sort, string.Empty, matrixId));

      builder.Entity<Control>().HasData(
        _control.GroupControl(Indicator.Group, Act.Add, groupId),
        _control.GroupControl(Indicator.Act, Act.Add, groupId),
        _control.GroupControl(Indicator.Icon, string.Empty, groupId),
        _control.GroupControl(Indicator.Sort, string.Empty, groupId));

      base.OnModelCreating(builder);
    }
  }
}