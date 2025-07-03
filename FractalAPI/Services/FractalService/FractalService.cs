using FractalAPI.Data;
using FractalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FractalAPI.Services
{
  public class FractalService(AppDbContext db) : IFractalService
  {
    private readonly AppDbContext _db = db;

    public async Task<Fractal> Get(string? cursor)
    {
      return await Find(cursor) ?? throw new Exception($"Unable to get fractal by id: {cursor}");
    }

    public async Task<Fractal?> Find(string? cursor)
    {
      return await _db.Fractals
        .Include(f => f.Controls)
        .FirstOrDefaultAsync(f => f.Cursor == cursor);
    }

    public async Task<Fractal> GetWithChildren(string cursor)
    {
      return await _db.Fractals
        .Include(f => f.Controls)
        .Include(f => f.Children)
        .FirstOrDefaultAsync(f => f.Cursor == cursor)
        ?? throw new Exception($"Unable to get fractal with children by cursor: {cursor}");
    }

    public async Task<Fractal> GetWithChildrenRecursively(string cursor)
    {
      Fractal fractal = await GetWithChildren(cursor);

      if (fractal.Children != null)
      {
        foreach (Fractal child in fractal.Children)
        {
          child.Children = (await GetWithChildrenRecursively(child.Cursor)).Children;
        }
      }

      return fractal;
    }

    public void DeleteWithChildrenRecursively(ICollection<Fractal>? children)
    {
      if (children != null)
      {
        foreach (var fractal in children)
        {
          if (fractal.Children != null) DeleteWithChildrenRecursively(fractal.Children);
          _db.Fractals.RemoveRange(children);
        }
      }
    }
  }
}
