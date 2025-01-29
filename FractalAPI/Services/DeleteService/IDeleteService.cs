using FractalAPI.Models;

namespace FractalAPI.Services
{
  public interface IDeleteService
  {
    void DeleteFractalChildrenRecursively(ICollection<Fractal> fractals);
  }
}
