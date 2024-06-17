using MatrixAPI.Dto;
using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public class MapService : IMapService
  {
    public Unit ToUnit(UnitDto dto, Guid? matrixId = null, Guid? unitId = null)
    {
      return new Unit
      {
        Id = dto.Id,
        Units = [],
        Controls = ToControls(dto.Controls),
        UnitId = unitId,
        MatrixId = matrixId,
      };
    }

    public Control ToControl(ControlDto dto, Guid? matrixId = null, Guid? unitId = null)
    {
      var (id, data, indicator) = dto;
      return new Control
      {
        Id = id,
        Data = data,
        Indicator = indicator,
        MatrixId = matrixId,
        UnitId = unitId,
      };
    }

    public ICollection<Control> ToControls(
      ControlDictionaryDto dto,
      Guid? matrixId = null,
      Guid? unitId = null
    )
    {
      ICollection<Control> controls = [];

      foreach (var value in dto.Values)
      {
        controls.Add(ToControl(value, matrixId, unitId));
      }

      return controls;
    }

    public MatrixDto ToMatrixDto(Matrix matrix)
    {
      return new MatrixDto(
        matrix.Id,
        matrix.Units.Select(ToUnitDto).ToList(),
        ToControlDto(matrix.Controls, matrix.Id)
      );
    }

    public UnitDto ToUnitDto(Unit unit)
    {
      return new UnitDto(
        unit.Id,
        unit.Units.Count == 0 ? [] : unit.Units.Select(ToUnitDto).ToList(),
        ToControlDto(unit.Controls, null, unit.Id)
      );
    }

    public ControlDictionaryDto ToControlDto(ICollection<Control> controls, Guid? matrixId = null, Guid? unitId = null)
    {
      var dic = new ControlDictionaryDto();
      foreach (var control in controls)
      {
        dic[control.Indicator] = new ControlDto(
          control.Id,
          control.Indicator,
          control.Data
        );
      }
      return dic;
    }
  }
}
