using FractalAPI.Dto;
using FractalAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace FractalAPI.Services
{
  public class MapService : IMapService
  {
    public Fractal ToFractal(FractalDto dto)
    {
      return new Fractal
      {
        Cursor = dto.Cursor,
        ParentCursor = dto.ParentCursor,
        Children = dto.Children?.Values.Select(ToFractal).ToList(),
        Controls = dto.Controls?.Values.Select(c => ToControl(c)).ToList(),
        ChildrenControls = dto.ChildrenControls?.Values.Select((c) => ToControl(c, true)).ToList(),
      };
    }

    public FractalDto ToFractalDto(Fractal fractal)
    {
      var childrenDto = fractal.Children?.ToDictionary(f => ToLowerFirstLetter(f.Cursor), ToFractalDto);
      var controlsDto = fractal.Controls?.ToDictionary(c => ToLowerFirstLetter(c.Cursor), ToControlDto);
      var chldrenControlsDto = fractal.ChildrenControls?.ToDictionary(c => ToLowerFirstLetter(c.Cursor), ToControlDto);

      return new FractalDto
      {
        Cursor = fractal.Cursor,
        ParentCursor = fractal.ParentCursor,
        Children = childrenDto.IsNullOrEmpty() ? null : childrenDto,
        Controls = controlsDto.IsNullOrEmpty() ? null : controlsDto,
        ChildrenControls = chldrenControlsDto.IsNullOrEmpty() ? null : chldrenControlsDto,
      };
    }

    public ControlDto ToControlDto(Control control)
    {
      return new ControlDto
      {
        Id = control.Id,
        Data = control.Data,
        Type = control.Type,
        Cursor = control.Cursor,
        ParentCursor = control.ControlParentCursor ?? control.ChildControlParentCursor,
      };
    }

    public Control ToControl(ControlDto dto, bool isChildControl = false)
    {
      return new Control
      {
        Id = dto.Id,
        Data = dto.Data,
        Type = dto.Type,
        Cursor = dto.Cursor,
        ControlParentCursor = isChildControl ? null : dto.ParentCursor,
        ChildControlParentCursor = isChildControl ? dto.ParentCursor : null,
      };
    }

    private static string ToLowerFirstLetter(string input)
    {
      if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
        return input;

      return char.ToLower(input[0]) + input[1..];
    }
  }
}
