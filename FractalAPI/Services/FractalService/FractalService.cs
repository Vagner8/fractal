using FractalAPI.Data;
using FractalAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace FractalAPI.Services
{
  public class FractalService(AppDbContext db) : IFractalService
  {
    private readonly AppDbContext _db = db;

    public FractalDto ToFractalDto(Fractal fractal)
    {
      return new FractalDto
      {
        Id = fractal.Id,
        ParentId = fractal.ParentId,
        Fractals = fractal.Fractals.Select(ToFractalDto).ToList(),
        Controls = fractal.Controls.Select(ToControlDto).ToList()
      };
    }

    public Fractal ToFractal(FractalDto dto)
    {
      return new Fractal
      {
        Id = dto.Id,
        ParentId = dto.ParentId,
        Fractals = dto.Fractals.Select(ToFractal).ToList(),
        Controls = dto.Controls.Select((dto) => ToControl(dto)).ToList()
      };
    }

    public async Task LoadFractalsRecursively(Fractal parent)
    {
      var children = await _db.Fractals.Where(u => u.ParentId == parent.Id).Include(u => u.Controls).ToListAsync();
      if (children.Count == 0) return;
      parent.Fractals = children;
      foreach (var child in children) await LoadFractalsRecursively(child);
    }

    private ControlDto ToControlDto(Control control)
    {
      return new ControlDto
      {
        Id = control.Id,
        ParentId = control.ParentId,
        Indicator = control.Indicator,
        Data = control.Data
      };
    }

    private Control ToControl(ControlDto dto, Guid? newId = null)
    {
      return new Control
      {
        Id = newId.HasValue ? newId : dto.Id,
        ParentId = dto.ParentId,
        Indicator = dto.Indicator,
        Data = dto.Data
      };
    }
  }
}