using FractalAPI.Models;

namespace FractalAPI.Services.ControlService
{
  public class ControlService : IControlService
  {
    public Control CreateControl(ControlDto dto)
    {
      return new Control
      {
        Id = dto.Id,
        ParentId = dto.ParentId,
        Indicator = dto.Indicator,
        Data = dto.Data,
        Field = dto.Field,
      };
    }

    public ControlDto CreateControlDto(Control control)
    {
      return new ControlDto
      {
        Id = control.Id,
        ParentId = control.ParentId,
        Indicator = control.Indicator,
        Data = control.Data,
        Field = control.Field,
      };
    }
  }
}
