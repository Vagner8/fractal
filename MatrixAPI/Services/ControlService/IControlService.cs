using MatrixAPI.Dto;

namespace MatrixAPI.Services
{
  public interface IControlService
  {
    Task Add(ControlDictionaryDto dto, Guid? matrixId = null, Guid? unitId = null);
    Task Update(ControlDictionaryDto dto);
    Task Delete(ControlDictionaryDto dto);
  }
}
