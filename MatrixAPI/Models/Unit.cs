using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
  public class Unit
  {
    [Key]
    public Guid? Id { get; set; }
    public List<Unit> Units { get; set; } = [];
    public ICollection<Control> Controls { get; set; } = [];

    [ForeignKey("ParentId")]
    public Guid? ParentId { get; set; }
    public Unit? Parent { get; set; }
  }
}
