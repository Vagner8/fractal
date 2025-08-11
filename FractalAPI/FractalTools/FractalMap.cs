using FractalAPI.ControlTools;
using FractalAPI.Dto;
using FractalAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace FractalAPI.FractalTools
{
  public static class FractalMap
  {
    public static Fractal ToFracal(FractalDto dto)
    {
      return new Fractal
      {
        Cursor = dto.Cursor,
        ParentCursor = dto.ParentCursor,
        Children = dto.Children?.Values.Select(ToFracal).ToList(),
        Controls = dto.Controls?.Values.Select(ControlMap.ToControl).ToList()
      };
    }

    public static FractalDto ToFracalDto(Fractal fractal)
    {
      var childrenDto = fractal.Children?.ToDictionary(f => f.Cursor, ToFracalDto);
      var controlsDto = fractal.Controls?.ToDictionary(c => c.Cursor, ControlMap.ToControlDto);

      return new FractalDto
      {
        Cursor = fractal.Cursor,
        ParentCursor = fractal.ParentCursor,
        Children = childrenDto.IsNullOrEmpty() ? null : childrenDto,
        Controls = controlsDto.IsNullOrEmpty() ? null : controlsDto,
      };
    }
  }
}
