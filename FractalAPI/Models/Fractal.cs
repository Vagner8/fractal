using System.ComponentModel.DataAnnotations;


namespace FractalAPI.Models
{
  public class Fractal : Base
  {
    [Key]
    public string Cursor { get; set; } = string.Empty;
    public ICollection<Fractal>? Children { get; set; }
    public ICollection<Control>? Controls { get; set; }
  }
}