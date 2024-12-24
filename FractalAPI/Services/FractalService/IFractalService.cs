using FractalAPI.Models;

namespace FractalAPI.Services
{
  public interface IFractalService
  {
    Fractal ToFractal(FractalDto dto);
    FractalDto ToFractalDto(Fractal fractal);
    public Control ToControl(ControlDto dto);
    Task LoadFractalsRecursively(Fractal parent);
  }
}