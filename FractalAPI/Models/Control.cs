using System.ComponentModel.DataAnnotations;

namespace FractalAPI.Models
{
  public class Control : Base
  {
    [Key]
    public Guid Id { get; set; }
    public string Cursor { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
  }
}