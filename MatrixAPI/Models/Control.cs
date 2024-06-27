using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatrixAPI.Models
{
  public class Control
  {
    [Key]
    public Guid? Id { get; set; }
    public string Indicator { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty;

    [ForeignKey("ParentId")]
    public Guid? ParentId { get; set; }
    public Unit? Parent { get; set; }
  }
}
