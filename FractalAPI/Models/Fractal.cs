using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FractalAPI.Models
{
  public class Fractal : Base
  {
    [Key] public string Cursor { get; set; } = string.Empty;
    public ICollection<Fractal>? Children { get; set; }
    public ICollection<Control>? Controls { get; set; }
    public ICollection<Control>? ChildrenControls { get; set; }

    [ForeignKey("ParentCursor")]
    public string? ParentCursor { get; set; }
  }
}