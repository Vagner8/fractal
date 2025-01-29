using FractalAPI.Models;

namespace FractalAPI.Services
{
  public interface IFractalService
  {
    ControlDto FindControl(FractalDto dto);
  }
}