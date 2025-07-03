using System.ComponentModel.DataAnnotations.Schema;

namespace FractalAPI.Models
{
  public class Base
  {
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [ForeignKey("ParentCursor")]
    public string? ParentCursor { get; set; } = string.Empty;
    public Fractal? Parent { get; set; }

  }
}