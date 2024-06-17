using MatrixAPI.Dto;

namespace MatrixAPI.Services
{
  public interface IUnitService
  {
    Task<UnitDto> Get(Guid unitId);
    Task Add(UnitDto dto);
    Task Delete(ICollection<UnitDto> dto);
  }
}
