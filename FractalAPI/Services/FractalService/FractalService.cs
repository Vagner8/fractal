using FractalAPI.Data;
using FractalAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace FractalAPI.Services
{
  public class FractalService(AppDbContext db) : IFractalService
  {
    private readonly AppDbContext _db = db;

    public async Task<FractalDto> Get(Guid fractalId)
    {
      var fractal = await _db.Fractals.Include(u => u.Controls)
        .FirstOrDefaultAsync(u => u.Id == fractalId) ?? throw new Exception("Fractal not found");
      await LoadFractalsRecursively(fractal);
      return ToFractalDto(fractal);
    }

    private async Task LoadFractalsRecursively(Fractal parent)
    {
      var children = await _db.Fractals.Where(u => u.ParentId == parent.Id).Include(u => u.Controls).ToListAsync();
      if (children.Count == 0) return;
      parent.Fractals = children;
      foreach (var child in children) await LoadFractalsRecursively(child);
    }

    public async Task Add(Fractal fractal)
    {
      var parent = await _db.Fractals.FindAsync(fractal.ParentId) ?? throw new Exception("Parent fractal not found");
      parent.Fractals.Add(fractal);
      await _db.Fractals.AddAsync(fractal);
      await _db.SaveChangesAsync();
    }

    public async Task Update(Fractal fractal)
    {
      _db.Fractals.Update(fractal);
      await _db.SaveChangesAsync();
    }

    public async Task Delete(ICollection<Fractal> fractals)
    {
      _db.Fractals.RemoveRange(fractals);
      await _db.SaveChangesAsync();
    }

    private FractalDto ToFractalDto(Fractal fractal)
    {
      return new FractalDto
      {
        Id = fractal.Id,
        Fractals = fractal.Fractals.Select(ToFractalDto).ToList(),
        Controls = fractal.Controls.Select(ToControlDto).ToList()
      };
    }

    private ControlDto ToControlDto(Control control)
    {
      return new ControlDto
      {
        Id = control.Id,
        Indicator = control.Indicator,
        Data = control.Data
      };
    }
  }
}