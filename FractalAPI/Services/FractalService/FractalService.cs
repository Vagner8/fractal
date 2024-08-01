using FractalAPI.Data;
using FractalAPI.Dto;
using FractalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FractalAPI.Services
{
  public class FractalService(AppDbContext db, IMapService maps) : IFractalService
  {
    private readonly AppDbContext _db = db;
    private readonly IMapService _maps = maps;

    public async Task<FractalDto> Get(Guid fractalId)
    {
      var fractal = await _db.Fractals.Include(u => u.Controls).FirstOrDefaultAsync(u => u.Id == fractalId) ?? throw new Exception("Fractal not found");
      await LoadFractalsRecursively(fractal);
      return _maps.ToFractalDto(fractal);
    }

    private async Task LoadFractalsRecursively(Fractal parent)
    {
      var children = await _db.Fractals.Where(u => u.ParentId == parent.Id).Include(u => u.Controls).ToListAsync();
      if (children.Count == 0) return;
      parent.Fractals = children;
      foreach (var child in children) await LoadFractalsRecursively(child);
    }

    public async Task Add(FractalDto dto)
    {
      var parent = await _db.Fractals.FindAsync(dto.ParentId) ?? throw new Exception("Parent fractal not found");
      var fractal = _maps.ToFractal(dto);
      parent.Fractals.Add(fractal);
      await _db.Fractals.AddAsync(fractal);
      await _db.SaveChangesAsync();
    }

    public async Task Update(FractalDto dto)
    {
      var fractal = _maps.ToFractal(dto);
      _db.Fractals.Update(fractal);
      await _db.SaveChangesAsync();
    }

    public async Task Delete(ICollection<FractalDto> dto)
    {
      var fractals = dto.Select(_maps.ToFractal);
      _db.Fractals.RemoveRange(fractals);
      await _db.SaveChangesAsync();
    }
  }
}
