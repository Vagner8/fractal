using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
  public class Unit
  {
    [Key]
    public Guid? Id { get; set; }
    public ICollection<Unit> Units { get; set; } = [];
    public ICollection<Control> Controls { get; set; } = [];

    [ForeignKey("UnitId")]
    public Guid? UnitId { get; set; }
    public Unit? Parent { get; set; }
  }
}
