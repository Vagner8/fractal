using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FractalAPI.Models
{
  public class Fractal
  {
    [Key]
    public Guid Id { get; set; }
    public ICollection<Fractal> Fractals { get; set; } = [];
    public ICollection<Control> Controls { get; set; } = [];

    [ForeignKey("ParentId")]
    public Guid? ParentId { get; set; }
    public Fractal? Parent { get; set; }

    public Control? FindControl(string indicator)
    {
      return Controls.FirstOrDefault(c => c.Indicator == indicator);
    }

    public Control GetControl(string indicator)
    {
      return FindControl(indicator)
        ?? throw new Exception($"Unable to get control by indicator: {indicator}");
    }
  }
}