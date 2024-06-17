using MatrixAPI.Dto;
using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public interface IMapService
  {
    Unit ToUnit(UnitDto unit, Guid? matrixId = null, Guid? unitId = null);
    Control ToControl(ControlDto dto, Guid? matrixId = null, Guid? unitId = null);
    ICollection<Control> ToControls(ControlDictionaryDto dto, Guid? matrixId = null, Guid? unitId = null);

    MatrixDto ToMatrixDto(Matrix matrix);
    UnitDto ToUnitDto(Unit unit);
    ControlDictionaryDto ToControlDto(
      ICollection<Control> controls,
      Guid? matrixId = null,
      Guid? unitId = null
    );
  }
}
