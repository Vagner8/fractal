using FractalAPI.Dto;
using FractalAPI.Models;

namespace FractalAPI.ControlTools
{
  public static class ControlMap
  {
    public static ControlDto ToControlDto(Control control)
    {
      return new ControlDto
      {
        Id = control.Id,
        Data = control.Data,
        Type = control.Type,
        Cursor = control.Cursor,
        ParentCursor = control.ParentCursor,
      };
    }

    public static Control ToControl(ControlDto controlDto)
    {
      return new Control
      {
        Id = controlDto.Id,
        Data = controlDto.Data,
        Type = controlDto.Type,
        Cursor = controlDto.Cursor,
        ParentCursor = controlDto.ParentCursor,
      };
    }
  }
}
