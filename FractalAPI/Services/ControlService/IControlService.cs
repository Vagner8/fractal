using FractalAPI.Models;

namespace FractalAPI.Services.ControlService
{
  public interface IControlService
  {
    Control CreateControl(ControlDto dto);
    ControlDto CreateControlDto(Control control);
  }
}
