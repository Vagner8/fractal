using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public class MapService : IMapService
  {
    public Matrix ToMatrix(MatrixDto matrixDto)
    {
      return new Matrix
      {
        Id = matrixDto.Id ?? Guid.NewGuid(),
        Units = matrixDto.Units.Select((u) => ToUnit(u, matrixDto.Id)).ToList(),
        Controls = matrixDto.Controls.Select((c) => ToControl(c, matrixDto.Id, null)).ToList(),
      };
    }

    public MatrixDto ToMatrixDto(Matrix matrix)
    {
      return new MatrixDto
      {
        Id = matrix.Id,
        Units = matrix.Units.Select(ToUnitDto).ToList(),
        Controls = matrix.Controls.Select(ToControlDto).ToList(),
      };
    }

    public Unit ToUnit(UnitDto unitDto, Guid? matrixId)
    {
      return new Unit
      {
        Id = unitDto.Id ?? Guid.NewGuid(),
        Controls = unitDto.Controls.Select((c) => ToControl(c, null, unitDto.Id)).ToList(),
        MatrixId = matrixId
      };
    }

    public UnitDto ToUnitDto(Unit unit)
    {
      return new UnitDto
      {
        Id = unit.Id,
        Controls = unit.Controls.Select(ToControlDto).ToList(),
      };
    }

    public Control ToControl(ControlDto controlDto, Guid? matrixId, Guid? unitId)
    {
      return new Control
      {
        Id = controlDto.Id ?? Guid.NewGuid(),
        Name = controlDto.Name,
        Data = controlDto.Data,
        Type = controlDto.Type,
        Act = controlDto.Act,
        MatrixId = matrixId,
        UnitId = unitId
      };
    }

    public ControlDto ToControlDto(Control control)
    {
      return new ControlDto
      {
        Id = control.Id,
        Name = control.Name,
        Data = control.Data,
        Type = control.Type,
        Act = control.Act,
      };
    }
  }

  public interface IMapService
  {
    Matrix ToMatrix(MatrixDto matrixDto);
    MatrixDto ToMatrixDto(Matrix matrix);
    Unit ToUnit(UnitDto unitDto, Guid? matrixId);
    UnitDto ToUnitDto(Unit unit);
    Control ToControl(ControlDto controlDto, Guid? matrixId, Guid? unitId);
    ControlDto ToControlDto(Control control);
  }
}
