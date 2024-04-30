using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
  public class Matrix
  {
    [Key]
    public Guid Id { get; set; }
    public List<Unit> Units { get; set; } = [];
    public List<Control> Controls { get; set; } = [];
  }

  public class MatrixDto
  {
    public Guid? Id { get; set; }
    public List<UnitDto> Units { get; set; } = [];
    public List<ControlDto> Controls { get; set; } = [];
  }
}
