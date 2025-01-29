using FractalAPI.Data;
using FractalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FractalAPI.Services
{
  public class GetService(AppDbContext db) : IGetService
  {
    private readonly AppDbContext _db = db;

    public async Task<Fractal?> FindFractal(Guid? id)
    {
      return await _db.Fractals
        .Include(f => f.Controls)
        .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Fractal> GetFractal(Guid? id)
    {
      return await FindFractal(id) ?? throw new Exception($"Unable to get fractal by id: {id}");
    }

    public async Task<Fractal> GetFractalWithChildren(Guid id)
    {
      return await _db.Fractals
        .Include(f => f.Controls)
        .Include(f => f.Fractals)
        .ThenInclude(f => f.Controls)
        .FirstOrDefaultAsync(f => f.Id == id)
        ?? throw new Exception($"Unable to get fractal with children by id: {id}");
    }

    public async Task<Fractal> GetFractalWithChildrenRecursively(Guid id)
    {
      Fractal fractal = await GetFractalWithChildren(id);

      if (fractal.Fractals != null)
      {
        foreach (Fractal child in fractal.Fractals)
        {
          child.Fractals = (await GetFractalWithChildrenRecursively(child.Id)).Fractals;
        }
      }

      return fractal;
    }
  }
}
