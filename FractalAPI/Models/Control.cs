using FractalAPI.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FractalAPI.Models
{
  public class Control
  {
    [Key]
    public Guid? Id { get; set; }
    public string Data { get; set; } = string.Empty;
    public string Input { get; set; } = string.Empty;
    public string Indicator { get; set; } = string.Empty;

    [ForeignKey("ParentId")]
    public Guid? ParentId { get; set; }
    public Fractal? Parent { get; set; }

    public void Push(string value)
    {
      EnsureSplitable();
      Data = string.Join(":", Data.Split(":").Append(value));
    }

    public void Remove(string value)
    {
      EnsureSplitable();
      Data = string.Join(":", Data.Split(":").Where(s => s != value));
    }

    private void EnsureSplitable()
    {
      if (typeof(SplitableIndicators).GetProperty(Indicator) != null)
        throw new Exception($"Unable to split: {Indicator}");
    }
  }
}