using FractalAPI.Models;
using System.Text.Json;

namespace FractalAPI.Data
{
  public partial class Seeding
  {
    private readonly ICollection<Fractal> _fractals = [];
    private readonly ICollection<Control> _controls = [];

    public Seeding()
    {
      Fractal fractal = GetData();
      _fractals.Add(new Fractal
      {
        Cursor = fractal.Cursor,
        ParentCursor = fractal.ParentCursor,
      });
      AddControls(fractal.Controls);
      AddFractals(fractal.Children);
    }

    public void Deconstruct(out ICollection<Fractal> fractals, out ICollection<Control> controls)
    {
      fractals = _fractals;
      controls = _controls;
    }

    private void AddFractals(ICollection<Fractal>? fractals)
    {
      if (fractals == null) return;

      foreach (var fractal in fractals)
      {
        _fractals.Add(new Fractal
        {
          Cursor = fractal.Cursor,
          ParentCursor = fractal.ParentCursor
        });

        AddControls(fractal.Controls);
        AddFractals(fractal.Children);
      }
    }

    private void AddControls(ICollection<Control>? controls)
    {
      if (controls != null)
      {
        foreach (var control in controls)
        {
          _controls.Add(new Control
          {
            Id = control.Id,
            Type = control.Type,
            Data = control.Data,
            Cursor = control.Cursor,
            ParentCursor = control.ParentCursor,
          });
        }
      }
    }

    private static Fractal GetData()
    {
      string path = Path.Combine("Data", "TestApp3.json");
      return JsonSerializer.Deserialize<Fractal>(File.ReadAllText(path)) ?? throw new Exception($"No data, path: {path}");
    }
  }
}