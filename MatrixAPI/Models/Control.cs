using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
  public class ControlBase
  {
    public required string Indicator { get; set; }
    public required string Data { get; set; }
  }

  public class Control : ControlBase
  {
    [Key]
    public Guid? Id { get; set; }

    [ConcurrencyCheck]
    public Guid Version { get; set; }

    [ForeignKey("MatrixId")]
    public Guid? MatrixId { get; set; }
    public Matrix Matrix { get; set; } = null!;
  }

  public class ControlDto : ControlBase
  {
    public Guid? Id { get; set; }
  }

  public class Act
  {
    public const string None = "None";
    public const string Add = "Add";
    public const string Update = "Update";
    public const string Remove = "Remove";
  }

  public class Indicator
  {
    public const string Matrix = "Matrix";
    public const string Group = "Group";
    public const string Icon = "Icon";
    public const string Sort = "Sort";
    public const string Act = "Act";
  }

  public class Prop
  {
    public static string? Id => nameof(ControlDto.Id);
    public static string Indicator => nameof(ControlBase.Indicator);
    public static string Data => nameof(ControlBase.Data);
  }
}
