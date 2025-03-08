using FractalAPI.Models;

namespace FractalAPI.Services
{
  public class MapService : IMapService
  {
    public Fractal ToFractal(FractalDto dto)
    {
      return new Fractal
      {
        Id = dto.Id,
        ParentId = dto.ParentId,
        Fractals = dto.Fractals?.Values.Select(ToFractal).ToList() ?? [],
        Controls = dto.Controls.Values.Select(ToControl).ToList()
      };
    }

    public FractalDto ToFractalDto(Fractal fractal)
    {
      Dictionary<string, FractalDto> fractals = [];
      Dictionary<string, ControlDto> controls = fractal.Controls.ToDictionary(c =>
        c.Indicator, ToControlDto);

      if (fractal.Fractals.Count == 0)
      {
        return new FractalDto
        {
          Id = fractal.Id,
          ParentId = fractal.ParentId,
          Fractals = null,
          Controls = controls,
        };
      }

      foreach (Fractal child in fractal.Fractals)
      {
        Control cursor = child.Controls.First(c => c.Indicator == "Cursor");
        fractals[cursor.Data] = ToFractalDto(child);
      }

      return new FractalDto
      {
        Id = fractal.Id,
        ParentId = fractal.ParentId,
        Fractals = fractals,
        Controls = controls,
      };
    }

    private static ControlDto ToControlDto(Control control)
    {
      return new ControlDto
      {
        Id = control.Id,
        ParentId = control.ParentId,
        Indicator = control.Indicator,
        Data = control.Data,
        Input = control.Input,
      };
    }

    public Control ToControl(ControlDto dto)
    {
      return new Control
      {
        Id = dto.Id,
        ParentId = dto.ParentId,
        Indicator = dto.Indicator,
        Data = dto.Data,
        Input = dto.Input,
      };
    }
  }
}
