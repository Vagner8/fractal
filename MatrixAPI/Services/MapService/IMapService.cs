using MatrixAPI.Dto;
using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public interface IMapService
  {
    Unit ToUnit(UnitDto unit);
    Control ToControl(ControlDto dto);
    ICollection<Control> ToControls(ControlDictionaryDto dto);

    UnitDto ToUnitDto(Unit unit);
    ControlDictionaryDto ToControlDto(ICollection<Control> controls);
  }
}
