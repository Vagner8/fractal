using FractalAPI.Dto;
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
        Fractals = [],
        Controls = ToControls(dto.Controls),
        ParentId = dto.ParentId,
      };
    }

    public Control ToControl(ControlDto dto)
    {
      return new Control
      {
        Id = dto.Id,
        Data = dto.Data,
        Indicator = dto.Indicator,
        ParentId = dto.ParentId,
      };
    }

    public ICollection<Control> ToControls(ControlDictionaryDto dto)
    {
      ICollection<Control> controls = [];

      foreach (var value in dto.Values)
      {
        controls.Add(ToControl(value));
      }

      return controls;
    }

    public FractalDto ToFractalDto(Fractal fractal)
    {
      FractalDictionaryDto dic = [];
      for (int i = 0; i < fractal.Fractals.Count; i++)
      {
        var subFractal = fractal.Fractals[i];
        var fractalName = subFractal.Controls.FirstOrDefault(c => c.Indicator == Indicator.Fractal)?.Data;

        if (!string.IsNullOrEmpty(fractalName))
        {
          dic[fractalName] = ToFractalDto(subFractal);
        }
        else
        {
          dic[i.ToString()] = ToFractalDto(subFractal);
        }
      }

      return new FractalDto(
        fractal.Id,
        fractal.ParentId,
        ToControlDto(fractal.Controls),
        dic.Count > 0 ? dic : null
      );
    }

    public ControlDictionaryDto ToControlDto(ICollection<Control> controls)
    {
      ControlDictionaryDto dic = [];
      foreach (var control in controls)
      {
        dic[control.Indicator] = new ControlDto(
          control.Id,
          control.ParentId,
          control.Indicator,
          control.Data
        );
      }
      return dic;
    }
  }
}