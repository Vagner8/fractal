using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
  public class ControlBase
  {
    public required string Name { get; set; }
    public required string Data { get; set; }
    public required string Type { get; set; }
    public required string Act { get; set; }
  }

  public class Control : ControlBase
  {
    [Key]
    public Guid Id { get; set; }

    [ConcurrencyCheck]
    public Guid Version { get; set; }

    [ForeignKey("MatrixId")]
    public Guid? MatrixId { get; set; }
    public Matrix Matrix { get; set; } = null!;

    [ForeignKey("UnitId")]
    public Guid? UnitId { get; set; }
    public Unit Unit { get; set; } = null!;
  }

  public class ControlDto : ControlBase
  {
    public Guid? Id { get; set; }
  }

  public class ControlAct
  {
    public const string None = "None";
    public const string Add = "Add";
    public const string Update = "Update";
    public const string Remove = "Remove";
  }

  public class ControlData
  {
    public const string Name = "Name";
    public const string Act = "Act";
  }

  public class ControlProp
  {
    public static string? Id => nameof(ControlDto.Id);
    public static string Name => nameof(ControlBase.Name);
    public static string Data => nameof(ControlBase.Data);
    public static string Type => nameof(ControlBase.Type);
    public static string Act => nameof(ControlBase.Act);
  }
}
