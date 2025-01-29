using FractalAPI.Models;

namespace FractalAPI.Services
{
  public interface IGetService
  {
    Task<Fractal?> FindFractal(Guid? id);
    Task<Fractal> GetFractal(Guid? id);
    Task<Fractal> GetFractalWithChildren(Guid id);
    Task<Fractal> GetFractalWithChildrenRecursively(Guid id);
  }
}
