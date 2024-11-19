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
      Guid id = Guid.NewGuid();
      _fractals.Add(new Fractal
      {
        Id = id,
        ParentId = null,
      });
      AddControls(fractal.Controls, id);
      AddFractals(fractal.Fractals, id);
    }

    public void Deconstruct(out ICollection<Fractal> fractals, out ICollection<Control> controls)
    {
      fractals = _fractals;
      controls = _controls;
    }

    private void AddFractals(ICollection<Fractal>? fractals, Guid? parentId)
    {
      if (fractals == null) return;

      foreach (var fractal in fractals)
      {
        Guid id = Guid.NewGuid();
        _fractals.Add(new Fractal
        {
          Id = id,
          ParentId = parentId
        });

        AddControls(fractal.Controls, id);
        AddFractals(fractal.Fractals, id);
      }
    }

    private void AddControls(ICollection<Control> controls, Guid parentId)
    {
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
      string path = Path.Combine("Data", "Data.json");
      return JsonSerializer.Deserialize<Fractal>(File.ReadAllText(path)) ?? throw new Exception($"No data, path: {path}");
    }
  }
}