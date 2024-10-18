namespace FractalAPI.Models
{
  public class FractalDto
  {
    public Guid? Id { get; set; }
    public List<FractalDto> Fractals { get; set; } = [];
    public ICollection<ControlDto> Controls { get; set; } = [];
  }
}
