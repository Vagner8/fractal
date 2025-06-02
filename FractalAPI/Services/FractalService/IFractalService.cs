using FractalAPI.Models;

namespace FractalAPI.Services.FractalService
{
  public interface IFractalService
  {
    Fractal CreateFractal(FractalDto dto);
    FractalDto CreateFractalDto(Fractal fractal);
    Task<Fractal?> FindFractal(Guid? id);
    Task<Fractal> GetFractal(Guid? id);
    Task<Fractal> GetFractalWithChildren(Guid id);
    Task<Fractal> GetFractalWithChildrenRecursively(Guid id);
    void DeleteFractalChildrenRecursively(ICollection<Fractal> fractals);
  }
}
