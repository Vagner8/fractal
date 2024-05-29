using System.ComponentModel.DataAnnotations;

namespace MatrixAPI.Models
{
  public class Matrix
  {
    [Key]
    public Guid? Id { get; set; }
    public List<Group> Groups { get; set; } = [];
    public List<Control> Controls { get; set; } = [];
  }

  public class MatrixDto
  {
    public Guid? Id { get; set; }
    public List<GroupDto> Groups { get; set; } = [];
    public List<ControlDto> Controls { get; set; } = [];
  }
}
