using FractalAPI.Models;

namespace FractalAPI.Services
{
  public interface IFractalService
  {
    Fractal ToFractal(FractalDto dto);
    FractalDto ToFractalDto(Fractal fractal);
    Task LoadFractalsRecursively(Fractal parent);
  }
}