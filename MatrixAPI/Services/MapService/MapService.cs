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

    public UnitDto ToUnitDto(Unit unit)
    {
      UnitDictionaryDto dic = [];
      for (int i = 0; i < unit.Units.Count; i++)
      {
        var subUnit = unit.Units[i];
        var unitName = subUnit.Controls.FirstOrDefault(c => c.Indicator == Indicator.Unit)?.Data;

        if (!string.IsNullOrEmpty(unitName))
        {
          dic[unitName] = ToUnitDto(subUnit);
        }
        else
        {
          dic[i.ToString()] = ToUnitDto(subUnit);
        }
      }

      return new UnitDto(
        unit.Id,
        unit.ParentId,
        ToControlDto(unit.Controls),
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