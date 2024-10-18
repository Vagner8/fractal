using FractalAPI.Data;
using FractalAPI.Models;

namespace FractalAPI.Services
{
  public class ControlService(AppDbContext db) : IControlService
  {
    private readonly AppDbContext _db = db;

    public async Task Add(Fractal fractal)
    {
      await _db.Controls.AddRangeAsync(fractal.Controls);
      await _db.SaveChangesAsync();
    }

    public async Task Update(Fractal fractal)
    {
      _db.Controls.UpdateRange(fractal.Controls);
      await _db.SaveChangesAsync();
    }

    public async Task Delete(Fractal fractal)
    {
      _db.Controls.RemoveRange(fractal.Controls);
      await _db.SaveChangesAsync();
    }
  }
}