using FractalAPI.Models;

namespace FractalAPI.Services
{
  public record SeedingResult(List<Fractal> Fractals, ICollection<Control> Controls);
}
