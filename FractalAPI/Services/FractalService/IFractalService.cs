using FractalAPI.Dto;

namespace FractalAPI.Services
{
  public interface IFractalService
  {
    Task<FractalDto> Get(Guid fractalId);
    Task Add(FractalDto dto);
    Task Update(FractalDto dto);
    Task Delete(ICollection<FractalDto> dto);
  }
}