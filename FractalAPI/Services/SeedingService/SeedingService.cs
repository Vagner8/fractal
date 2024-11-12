using FractalAPI.Models;
using System.Text.Json;

namespace FractalAPI.Services
{
  public partial class SeedingService
  {
    private readonly ICollection<Fractal> _fractals = [];
    private readonly ICollection<Control> _controls = [];

    public SeedingService()
    {
      var fractal = GetData();
      var id = Guid.NewGuid();
      _fractals.Add(new Fractal
      {
        Id = id,
        ParentId = null,
      });
      Controls(fractal.Controls, id);
      Fractals(fractal.Fractals, id);
    }

    public void Deconstruct(out ICollection<Fractal> fractals, out ICollection<Control> controls)
    {
      fractals = _fractals;
      controls = _controls;
    }

    private void Fractals(ICollection<Fractal>? fractals, Guid? parentId)
    {
      if (fractals == null) return;
      foreach (var fractal in fractals)
      {
        var id = Guid.NewGuid();
        _fractals.Add(new Fractal
        {
          Id = id,
          ParentId = parentId,
        });
        Controls(fractal.Controls, id);
        Fractals(fractal.Fractals, id);
      }
    }

    private void Controls(ICollection<Control>? controls, Guid parentId)
    {
      if (controls == null) return;
      foreach (var control in controls)
      {
        _controls.Add(new Control
        {
          Id = Guid.NewGuid(),
          ParentId = parentId,
          Data = control.Data,
          Indicator = control.Indicator,
        });
      }
    }

    private static Fractal GetData()
    {
      string path = Path.Combine("Services", "SeedingService", "Data.json");
      string json = File.ReadAllText(path);
      return JsonSerializer.Deserialize<Fractal>(json) ?? throw new Exception($"No data, path: {path}");
    }
  }
}