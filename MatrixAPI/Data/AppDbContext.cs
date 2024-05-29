using MatrixAPI.Models;
using MatrixAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
      Seeding(builder);
      Delete(builder);
      base.OnModelCreating(builder);
    }

    private void Seeding(ModelBuilder builder)
    {
      var matrixId = Guid.NewGuid();
      var groupId = Guid.NewGuid();
      var unitId = Guid.NewGuid();

      builder.Entity<Matrix>().HasData(new Matrix
      {
        Id = matrixId,
      });

      builder.Entity<Group>().HasData(new Group
      {
        Id = groupId,
        MatrixId = matrixId,
      });

      builder.Entity<Unit>().HasData(new Unit
      {
        Id = unitId,
        GroupId = groupId,
      });

      builder.Entity<Control>().HasData(
        _control.MatrixControl(Indicator.Matrix, Act.Add, matrixId),
        _control.MatrixControl(Indicator.Icon, string.Empty, matrixId),
        _control.MatrixControl(Indicator.Sort, string.Empty, matrixId));

      builder.Entity<Control>().HasData(
        _control.GroupControl(Indicator.Act, Act.Add, groupId),
        _control.GroupControl(Indicator.Group, Act.Add, groupId),
        _control.GroupControl(Indicator.Icon, string.Empty, groupId),
        _control.GroupControl(Indicator.Sort, string.Empty, groupId));

      builder.Entity<Control>().HasData(
        _control.UnitControl(Indicator.Act, Act.Add, unitId),
        _control.UnitControl("Name", "Dima", unitId));
    }

    private static void Delete(ModelBuilder builder)
    {
      builder.Entity<Unit>()
          .HasMany(u => u.Controls)
          .WithOne(c => c.Unit)
          .OnDelete(DeleteBehavior.Cascade);
    }
  }
}