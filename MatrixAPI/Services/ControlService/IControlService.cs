using MatrixAPI.Dto;

namespace MatrixAPI.Services
{
  public interface IControlService
  {
    Task Add(ControlDictionaryDto dto);
    Task Update(ControlDictionaryDto dto);
    Task Delete(ControlDictionaryDto dto);
  }
}
