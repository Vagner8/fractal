using FractalAPI.Models;

namespace FractalAPI.Services
{
  public interface IControlService
  {
    Task Add(Fractal fractal);
    Task Update(Fractal fractal);
    Task Delete(Fractal fractal);
  }
}