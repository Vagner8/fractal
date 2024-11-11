namespace FractalAPI.Models
{
  public class FractalDto
  {
    public Guid? Id { get; set; }
    public Guid? ParentId { get; set; }
    public ICollection<FractalDto> Fractals { get; set; } = [];
    public ICollection<ControlDto> Controls { get; set; } = [];
  }
}
