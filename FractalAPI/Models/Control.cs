using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FractalAPI.Models
{
  public class Control : Base
  {
    [Key] public Guid Id { get; set; }
    public string Cursor { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string? ParentCursor { get; set; }

    [ForeignKey("ControlParentCursor")]
    public string? ControlParentCursor { get; set; }
    [ForeignKey("ChildControlParentCursor")]
    public string? ChildControlParentCursor { get; set; }
  }
}