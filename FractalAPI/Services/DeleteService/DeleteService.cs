using FractalAPI.Data;
using FractalAPI.Models;

namespace FractalAPI.Services
{
  public class DeleteService(AppDbContext db, IGetService gs) : IDeleteService
  {
    private readonly AppDbContext _db = db;
    private readonly IGetService _gs = gs;

    public void DeleteFractalChildrenRecursively(ICollection<Fractal> fractals)
    {
      foreach (var fractal in fractals)
      {
        if (fractal.Fractals != null) DeleteFractalChildrenRecursively(fractal.Fractals);
        _db.Fractals.RemoveRange(fractals);
      }
    }
  }
}
