using FractalAPI.Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace FractalAPI.Services
{
  public partial class SeedingService : ISeedingService
  {
    private readonly string _dataPath = Path.Combine("Services", "SeedingService", "Data.json");
    private readonly SeedingResult _result = new([], []);

    public SeedingResult Data()
    {
      _result.Fractals.Clear();
      _result.Controls.Clear();
      var fractal = GetData();
      var id = Guid.NewGuid();
      _result.Fractals.Add(new Fractal
      {
        Id = id,
        ParentId = null,
      });
      Controls(fractal.Controls, id);
      Fractals(fractal.Fractals, id);
      return _result;
    }

    private void Fractals(List<Fractal> fractals, Guid? parentId)
    {
      foreach (var fractal in fractals)
      {
        var id = Guid.NewGuid();
        _result.Fractals.Add(new Fractal
        {
          Id = id,
          ParentId = parentId,
        });
        Controls(fractal.Controls, id);

        if (fractal.Fractals != null) Fractals(fractal.Fractals, id);
      }
    }

    private void Controls(ICollection<Control>? controls, Guid parentId)
    {
      if (controls == null) return;
      foreach (var control in controls)
      {
        _result.Controls.Add(new Control
        {
          Id = Guid.NewGuid(),
          ParentId = parentId,
          Data = control.Data,
          Indicator = control.Indicator,
        });
      }
    }

    private Fractal GetData()
    {
      var json = File.ReadAllText(_dataPath);
      var noCommentsJson = SearchComments().Replace(json, string.Empty);
      var fractal = JsonSerializer.Deserialize<Fractal>(noCommentsJson);
      return fractal ?? throw new Exception($"No data, path: {_dataPath}");
    }

    [GeneratedRegex(@"//.*(?=\r?\n)|/\*[\s\S]*?\*/")]
    private static partial Regex SearchComments();
  }
}