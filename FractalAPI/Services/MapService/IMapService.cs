using FractalAPI.Models;

namespace FractalAPI.Services
{
  public interface IMapService
  {
    Fractal ToFractal(FractalDto dto);
    Control ToControl(ControlDto dto);
    FractalDto ToFractalDto(Fractal fractal);
  }
}
