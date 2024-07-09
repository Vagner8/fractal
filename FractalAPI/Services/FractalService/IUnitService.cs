using FractalAPI.Dto;

namespace FractalAPI.Services
{
  public interface IFractalController
  {
    Task<FractalDto> Get(Guid fractalId);
    Task Add(FractalDto dto);
    Task Delete(ICollection<FractalDto> dto);
  }
}
