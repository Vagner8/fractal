using System.ComponentModel.DataAnnotations;

namespace MatrixAPI.Models
{
  public class Matrix
  {
    [Key]
    public Guid Id { get; set; }
    public List<Group> Groups { get; set; } = [];
    public List<Control> Controls { get; set; } = [];
  }

  public record MatrixDto(Guid Id, List<GroupDto>? Groups, List<ControlDto>? Controls);
}
