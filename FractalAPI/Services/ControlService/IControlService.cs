using FractalAPI.Dto;

namespace FractalAPI.Services
{
  public interface IControlService
  {
    Task Add(ControlDictionaryDto dto);
    Task Update(ControlDictionaryDto dto);
    Task Delete(ControlDictionaryDto dto);
  }
}