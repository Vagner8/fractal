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

      foreach (Fractal child in fractal.Fractals)
      {
        Control? cursor = child.Controls.FirstOrDefault(c =>
          c.Indicator == Indicators.Cursor);

        if (cursor != null)
        {
          fractals[cursor.Data] = ToFractalDto(child);
        }
        else
        {
          Control? position = child.Controls.FirstOrDefault(c =>
          c.Indicator == Indicators.Position);
          fractals[position != null ? position.Data : ""] = ToFractalDto(child);
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
        Data = ToDataDto(control.Data)
      };
    }

    private static Control ToControl(ControlDto dto)
    {
      return new Control
      {
        Id = dto.Id,
        ParentId = dto.ParentId,
        Indicator = dto.Indicator,
        Data = ToData(dto.Data)
      };
    }

    private static object ToDataDto(string data)
    {
      if (data == "true") return true;
      if (data == "false") return false;
      return data;
    }

    private static string ToData(object data)
    {
      if (data is bool boolData) return boolData ? "true" : "false";
      return data?.ToString() ?? string.Empty;
    }
  }
}