using FractalAPI.Dto;
using FractalAPI.Models;

namespace FractalAPI.Services
{
  public interface IMapService
  {
    Fractal ToFractal(FractalDto fractal);
    Control ToControl(ControlDto dto);
    ICollection<Control> ToControls(ControlDictionaryDto dto);

    FractalDto ToFractalDto(Fractal fractal);
    ControlDictionaryDto ToControlDto(ICollection<Control> controls);
  }
}