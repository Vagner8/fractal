using MatrixAPI.Dto;

namespace MatrixAPI.Services
{
  public interface IUnitService
  {
    Task Add(UnitDto dto, Guid? matrixId, Guid? unitId);
    Task Delete(ICollection<UnitDto> dto);
  }
}
