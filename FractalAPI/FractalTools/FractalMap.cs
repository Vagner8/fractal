using FractalAPI.ControlTools;
using FractalAPI.Dto;
using FractalAPI.Models;

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
      var childFractalsDto = new Dictionary<string, FractalDto>();
      var controlsDto = fractal.Controls?.ToDictionary(c => c.Cursor, ControlMap.ToControlDto);

      if (fractal.Children != null)
      {
        foreach (var child in fractal.Children)
        {
          childFractalsDto[child.Cursor] = ToFracalDto(child);
        }
      }

      return new FractalDto
      {
        Cursor = fractal.Cursor,
        ParentCursor = fractal.ParentCursor,
        Children = childFractalsDto,
        Controls = controlsDto,
      };
    }
  }
}
