using MatrixAPI.Dto;
using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public class MapService : IMapService
  {
    public Unit ToUnit(UnitDto dto)
    {
      return new Unit
      {
        Id = dto.Id,
        Units = [],
        Controls = ToControls(dto.Controls),
        UnitId = dto.UnitId,
      };
    }

    public Control ToControl(ControlDto dto)
    {
      return new Control
      {
        Id = dto.Id,
        Data = dto.Data,
        Indicator = dto.Indicator,
        UnitId = dto.UnitId,
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

    public UnitDto ToUnitDto(Unit unit)
    {
      return new UnitDto(
        unit.Id,
        unit.UnitId,
        unit.Units.Count == 0 ? [] : unit.Units.Select(ToUnitDto).ToList(),
        ToControlDto(unit.Controls)
      );
    }

    public ControlDictionaryDto ToControlDto(ICollection<Control> controls)
    {
      var dic = new ControlDictionaryDto();
      foreach (var control in controls)
      {
        dic[control.Indicator] = new ControlDto(
          control.Id,
          control.UnitId,
          control.Indicator,
          control.Data
        );
      }
      return dic;
    }
  }
}
