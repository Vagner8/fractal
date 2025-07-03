using FractalAPI.Models;

namespace FractalAPI.Services
{
  public interface IFractalService
  {
    Task<Fractal> Get(string? cursor);
    Task<Fractal?> Find(string? cursor);
    Task<Fractal> GetWithChildren(string cursor);
    Task<Fractal> GetWithChildrenRecursively(string cursor);
    void DeleteWithChildrenRecursively(ICollection<Fractal>? fractals);
  }
}
