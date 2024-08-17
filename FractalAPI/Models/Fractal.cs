using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FractalAPI.Models
{
  public class Fractal
  {
    [Key]
    public Guid? Id { get; set; }
    public List<Fractal> Fractals { get; set; } = [];
    public ICollection<Control> Controls { get; set; } = [];

    [ForeignKey("ParentId")]
    public Guid? ParentId { get; set; }
    public Fractal? Parent { get; set; }
  }
}