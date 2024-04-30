using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public class FilterService(IMapService map, IControlService control) : IFilterService
  {
    private readonly IMapService _map = map;
    private readonly IControlService _control = control;
    private readonly Filtered _filtered = new();

    public Filtered MatrixDto(List<MatrixDto> matricesDto)
    {
      MatricesUpdate(matricesDto.Select(_map.ToMatrix).ToList());
      return _filtered;
    }

    private void MatricesUpdate(List<Matrix> matrices)
    {
      foreach (var matrix in matrices)
      {
        var controlAct = _control.FindAct(matrix.Controls);
        if (controlAct == null) continue;
        {
          switch (controlAct.Data)
          {
            case ControlAct.Add:
              _filtered.MatricesToAdd.Add(matrix);
              break;
            case ControlAct.Remove:
              _filtered.MatricesToRemove.Add(matrix);
              break;
            case ControlAct.Update:
              ControlsUpdate(matrix.Controls);
              UnitsUpdate(matrix.Units);
              break;
          }
        }
      }
    }

    private void UnitsUpdate(List<Unit> units)
    {
      foreach (var Unit in units)
      {
        var controlAct = _control.FindAct(Unit.Controls);
        if (controlAct == null) continue;
        {
          switch (controlAct.Data)
          {
            case ControlAct.Add:
              _filtered.UnitsToAdd.Add(Unit);
              break;
            case ControlAct.Remove:
              _filtered.UnitsToRemove.Add(Unit);
              break;
            case ControlAct.Update:
              ControlsUpdate(Unit.Controls);
              break;
          }
        }
      }
    }

    private void ControlsUpdate(List<Control> controls)
    {
      foreach (var control in controls)
      {
        switch (control.Act)
        {
          case ControlAct.Add:
            _filtered.ControlsToAdd.Add(control);
            break;
          case ControlAct.Update:
            _filtered.ControlsToUpdate.Add(control);
            break;
          case ControlAct.Remove:
            _filtered.ControlsToRemove.Add(control);
            break;
        }
      }
    }
  }

  public class Filtered
  {
    public List<Matrix> MatricesToAdd = [];
    public List<Matrix> MatricesToUpdate = [];
    public List<Matrix> MatricesToRemove = [];

    public List<Unit> UnitsToAdd = [];
    public List<Unit> UnitsToUpdate = [];
    public List<Unit> UnitsToRemove = [];

    public List<Control> ControlsToAdd = [];
    public List<Control> ControlsToUpdate = [];
    public List<Control> ControlsToRemove = [];
  }

  public interface IFilterService
  {
    Filtered MatrixDto(List<MatrixDto> matricesDto);
  }
}
