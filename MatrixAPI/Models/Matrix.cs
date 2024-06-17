using System.ComponentModel.DataAnnotations;

namespace MatrixAPI.Models
{
  public class Matrix
  {
    [Key]
    public Guid Id { get; set; }
    public ICollection<Unit> Units { get; set; } = [];
    public ICollection<Control> Controls { get; set; } = [];
  }
}
