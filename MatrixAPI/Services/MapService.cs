using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public class MapService : IMapService
  {
    public Matrix ToMatrix(MatrixDto matrixDto)
    {
      return new Matrix
      {
        Id = matrixDto.Id,
        Groups = matrixDto.Groups.Select((t) => ToTable(t, matrixDto.Id)).ToList(),
        Controls = matrixDto.Controls.Select((c) => ToControl(c, matrixDto.Id, null, null)).ToList(),
      };
    }

    public Group ToTable(GroupDto screenDto, Guid? matrixId)
    {
      return new Group
      {
        Id = screenDto.Id,
        Units = screenDto.Units.Select(u => ToUnit(u, screenDto.Id)).ToList(),
        Controls = screenDto.Controls.Select(c => ToControl(c, null, screenDto.Id, null)).ToList(),
        MatrixId = matrixId,
      };
    }

    public Unit ToUnit(UnitDto unitDto, Guid? screenId)
    {
      return new Unit
      {
        Id = unitDto.Id ?? Guid.NewGuid(),
        Controls = unitDto.Controls.Select((c) => ToControl(c, null,null, unitDto.Id)).ToList(),
        GroupId = screenId
      };
    }

    public Control ToControl(ControlDto controlDto, Guid? matrixId, Guid? screenId, Guid? unitId)
    {
      return new Control
      {
        Id = controlDto.Id,
        Indicator = controlDto.Indicator,
        Data = controlDto.Data,
        MatrixId = matrixId,
        GroupId = screenId,
        UnitId = unitId
      };
    }

    public MatrixDto ToMatrixDto(Matrix matrix)
    {
      return new MatrixDto
      {
        Id = matrix.Id,
        Groups = matrix.Groups.Select(ToTableDto).ToList(),
        Controls = matrix.Controls.Select(ToControlDto).ToList(),
      };
    }

    public GroupDto ToTableDto(Group group)
    {
      return new GroupDto
      {
        Id = group.Id,
        Units = group.Units.Select(ToUnitDto).ToList(),
        Controls = group.Controls.Select(ToControlDto).ToList(),
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

    public ControlDto ToControlDto(Control control)
    {
      return new ControlDto
      {
        Id = control.Id,
        Indicator = control.Indicator,
        Data = control.Data
      };
    }
  }

  public interface IMapService
  {
    Matrix ToMatrix(MatrixDto matrixDto);
    Group ToTable(GroupDto screenDto, Guid? matrixId);
    Unit ToUnit(UnitDto unitDto, Guid? matrixId);
    Control ToControl(ControlDto controlDto, Guid? matrixId, Guid? screenId, Guid? unitId);

    MatrixDto ToMatrixDto(Matrix matrix);
    GroupDto ToTableDto(Group group);
    UnitDto ToUnitDto(Unit unit);
    ControlDto ToControlDto(Control control);
  }
}
