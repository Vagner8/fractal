using System.ComponentModel.DataAnnotations;

namespace MatrixAPI.Models
{
  public class Unit
  {
    [Key]
    public Guid Id { get; set; }
    public List<Unit>? Units { get; set; } = [];
    public List<Control> Controls { get; set; } = [];
  }

  public class UnitDto
  {
    public Guid? Id { get; set; }
    public List<UnitDto>? Units { get; set; } = [];
    public List<ControlDto> Controls { get; set; } = [];
  }
}
