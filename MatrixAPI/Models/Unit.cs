using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
  public class Unit
  {
    [Key]
    public Guid Id { get; set; }
    public List<Control> Controls { get; set; } = [];

    [ForeignKey("MatrixId")]
    public Guid? MatrixId { get; set; }
    public Matrix Matrix { get; set; } = null!;
  }

  public class UnitDto
  {
    public Guid? Id { get; set; }
    public List<ControlDto> Controls { get; set; } = [];
  }
}
