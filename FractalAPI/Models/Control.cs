using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FractalAPI.Models
{
  public class Control
  {
    [Key]
    public Guid? Id { get; set; }
    public string Data { get; set; } = string.Empty;
    public string Field { get; set; } = string.Empty;
    public string Indicator { get; set; } = string.Empty;

    [ForeignKey("ParentId")]
    public Guid? ParentId { get; set; }
    public Fractal? Parent { get; set; }
  }
}