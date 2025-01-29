using FractalAPI.Data;
using FractalAPI.Models;

namespace FractalAPI.Services
{
  public class UpdateService(AppDbContext db)
  {
    private readonly AppDbContext _db = db;

    public void UpdateParentSort(Guid parentId)
    {
      Fractal fractal = _db.Fractals.FirstOrDefault(f => f.Id == parentId)
        ?? throw new Exception("Parent not found");
    }
  }
}
