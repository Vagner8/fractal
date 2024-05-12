using System.ComponentModel.DataAnnotations;

namespace MatrixAPI.Models
{
  public class Matrix
  {
    [Key]
    public Guid? Id { get; set; }
    public List<Matrix>? Matrixes { get; set; } = [];
    public List<Control> Controls { get; set; } = [];
  }

  public class MatrixDto
  {
    public Guid? Id { get; set; }
    public List<MatrixDto>? Matrixes { get; set; } = [];
    public List<ControlDto> Controls { get; set; } = [];
  }
}
