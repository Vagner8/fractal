using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
  public class Unit
  {
    [Key]
    public Guid? Id { get; set; }
    public List<Control> Controls { get; set; } = [];

    [ForeignKey("GroupId")]
    public Guid? GroupId { get; set; }
    public Group Group { get; set; } = null!;
  }

  public record UnitDto(Guid? Id, List<ControlDto>? Controls);
}
