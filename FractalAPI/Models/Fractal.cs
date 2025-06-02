namespace FractalAPI.Models
{
  public class Fractal : CommonEntity
  {
    public ICollection<Fractal> Fractals { get; set; } = [];
    public ICollection<Control> Controls { get; set; } = [];
  }
}