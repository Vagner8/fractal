namespace FractalAPI.Models
{
  public class FractalDto
  {
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public Dictionary<string, FractalDto>? Fractals { get; set; } = null;
    public Dictionary<string, ControlDto> Controls { get; set; } = [];
  }
}
