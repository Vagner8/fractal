using FractalAPI.Models;
using System.Text.Json;

namespace FractalAPI.Data
{
  public partial class Seeding
  {
    private readonly ICollection<Fractal> _children = [];
    private readonly ICollection<Control> _controls = [];
    private readonly ICollection<Control> _childrenControls = [];

    public Seeding()
    {
      Fractal fractal = GetData();
      _children.Add(new Fractal
      {
        Cursor = fractal.Cursor,
        ParentCursor = fractal.ParentCursor,
      });
      AddChildren(fractal.Children);
      AddControls(fractal.Controls);
      AddChildrenControls(fractal.ChildrenControls);
    }

    public void Deconstruct(
      out ICollection<Fractal> fractals,
      out ICollection<Control> controls,
      out ICollection<Control> childrenControls)
    {
      fractals = _children;
      controls = _controls;
      childrenControls = _childrenControls;
    }

    private void AddChildren(ICollection<Fractal>? fractals)
    {
      if (fractals == null) return;

      foreach (var fractal in fractals)
      {
        _children.Add(new Fractal
        {
          Cursor = fractal.Cursor,
          ParentCursor = fractal.ParentCursor
        });

        AddChildren(fractal.Children);
        AddControls(fractal.Controls);
        AddChildrenControls(fractal.ChildrenControls);
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
            ControlParentCursor = control.ParentCursor,
          });
        }
      }
    }

    private void AddChildrenControls(ICollection<Control>? controls)
    {
      if (controls != null)
      {
        foreach (var control in controls)
        {
          _childrenControls.Add(new Control
          {
            Id = control.Id,
            Type = control.Type,
            Data = control.Data,
            Cursor = control.Cursor,
            ChildControlParentCursor = control.ParentCursor,
          });
        }
      }
    }

    private static Fractal GetData()
    {
      string path = Path.Combine("Data", "TestApp.json");
      return JsonSerializer.Deserialize<Fractal>(File.ReadAllText(path)) ?? throw new Exception($"No data, path: {path}");
    }
  }
}