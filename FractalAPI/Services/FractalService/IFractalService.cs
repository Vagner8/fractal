using FractalAPI.Models;

namespace FractalAPI.Services
{
  public interface IFractalService
  {
    Task<FractalDto> Get(Guid fractalId);
    Task Add(Fractal fractal);
    Task Update(Fractal fractal);
    Task Delete(ICollection<Fractal> fractal);
  }
}