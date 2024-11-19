using FractalAPI.Data;
using FractalAPI.Enums;
using FractalAPI.Models;

using Microsoft.EntityFrameworkCore;

namespace FractalAPI.Services
{
  public class FractalService(AppDbContext db) : IFractalService
  {
    private readonly AppDbContext _db = db;

    public Fractal ToFractal(FractalDto dto)
    {
      return new Fractal
      {
        Id = dto.Id,
        ParentId = dto.ParentId,
        Fractals = dto.Fractals?.Values.Select(ToFractal).ToList(),
        Controls = dto.Controls.Values.Select(ToControl).ToList()
      };
    }

    public FractalDto ToFractalDto(Fractal fractal)
    {
      int index = 0;
      Dictionary<string, FractalDto> fractals = [];
      Dictionary<string, ControlDto> controls = fractal.Controls.ToDictionary(c =>
        c.Indicator, ToControlDto);

      if (fractal.Fractals == null)
      {
        return new FractalDto
        {
          Id = fractal.Id,
          ParentId = fractal.ParentId,
          Fractals = null,
          Controls = controls,
        };
      }

      foreach (var child in fractal.Fractals)
      {
        var cursor = child.Controls.FirstOrDefault(c =>
          c.Indicator == Indicators.Cursor.ToString());

        if (cursor != null)
        {
          fractals[cursor.Data] = ToFractalDto(child);
        }
        else
        {
          fractals[index.ToString()] = ToFractalDto(child);
          index++;
        }
      }

      return new FractalDto
      {
        Id = fractal.Id,
        ParentId = fractal.ParentId,
        Fractals = fractals,
        Controls = controls,
      };
    }

    public async Task LoadFractalsRecursively(Fractal parent)
    {
      var children = await _db.Fractals.Where(u => u.ParentId == parent.Id).Include(u => u.Controls).ToListAsync();
      if (children.Count == 0) return;
      parent.Fractals = children;
      foreach (var child in children) await LoadFractalsRecursively(child);
    }

    private static ControlDto ToControlDto(Control control)
    {
      return new ControlDto
      {
        Id = control.Id,
        ParentId = control.ParentId,
        Indicator = control.Indicator,
        Data = control.Data
      };
    }

    private static Control ToControl(ControlDto dto)
    {
      return new Control
      {
        Id = dto.Id,
        ParentId = dto.ParentId,
        Indicator = dto.Indicator,
        Data = dto.Data
      };
    }
  }
}