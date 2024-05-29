using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public class MapService : IMapService
  {
    public Matrix ToMatrix(MatrixDto matrix)
    {
      return new Matrix
      {
        Id = matrix.Id,
        Groups = ToGroups(matrix.Groups, matrix.Id),
        Controls = ToControls(matrix.Controls, matrix.Id, null, null),
      };
    }

    public List<Group> ToGroups(List<GroupDto> groups, Guid? matrixId)
    {
      return groups.Select(g => new Group
      {
        Id = g.Id,
        Units = ToUnits(g.Units, g.Id),
        Controls = ToControls(g.Controls, null, g.Id, null),
        MatrixId = matrixId,
      }).ToList();
    }

    public List<Unit> ToUnits(List<UnitDto> units, Guid? groupId)
    {
      return units.Select(u => new Unit
      {
        Id = u.Id,
        Controls = ToControls(u.Controls, null, null, u.Id),
        GroupId = groupId
      }).ToList();
    }

    public List<Control> ToControls(List<ControlDto> controls, Guid? matrixId, Guid? groupId, Guid? unitId)
    {
      return controls.Select(c => new Control
      {
        Id = c.Id,
        Indicator = c.Indicator,
        Data = c.Data,
        MatrixId = matrixId,
        GroupId = groupId,
        UnitId = unitId,
      }).ToList();
    }

    public MatrixDto ToMatrixDto(Matrix matrix)
    {
      return new MatrixDto
      {
        Id = matrix.Id,
        Groups = ToGroupsDto(matrix.Groups),
        Controls = ToControlsDto(matrix.Controls),
      };
    }

    public List<GroupDto> ToGroupsDto(List<Group> groups)
    {
      return groups.Select(g => new GroupDto
      {
        Id = g.Id,
        Units = ToUnitsDto(g.Units),
        Controls = ToControlsDto(g.Controls),
      }).ToList();
    }

    public List<UnitDto> ToUnitsDto(List<Unit> units)
    {
      return units.Select(u => new UnitDto
      {
        Id = u.Id,
        Controls = ToControlsDto(u.Controls),
      }).ToList();
    }

    public List<ControlDto> ToControlsDto(List<Control> controls)
    {
      return controls.Select(c => new ControlDto
      {
        Id = c.Id,
        Indicator = c.Indicator,
        Data = c.Data
      }).ToList();
    }
  }

  public interface IMapService
  {
    Matrix ToMatrix(MatrixDto matrix);
    List<Group> ToGroups(List<GroupDto> groups, Guid? matrixId);
    List<Unit> ToUnits(List<UnitDto> units, Guid? groupId);
    List<Control> ToControls(List<ControlDto> controls, Guid? matrixId, Guid? groupId, Guid? unitId);
    
    MatrixDto ToMatrixDto(Matrix matrix);
    List<GroupDto> ToGroupsDto(List<Group> groups);
    List<UnitDto> ToUnitsDto(List<Unit> units);
    List<ControlDto> ToControlsDto(List<Control> controls);
  }
}
