using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public class WorkService(IMapService map, IControlService control) : IWorkService
  {
    private readonly IMapService _map = map;
    private readonly IControlService _control = control;
    private readonly Work _work = new();

    public Work OnInit(MatrixDto matrixDto)
    {
      MatrixWork(_map.ToMatrix(matrixDto));
      return _work;
    }

    private void MatrixWork(Matrix matrix)
    {
      var actControl = _control.FindAct(matrix.Controls);
      if (actControl == null) return;
      switch (actControl.Data)
      {
        case Act.Add:
          _work.MatrixToAdd = matrix;
          break;
        case Act.Update:
          GroupsWork(matrix.Groups);
          ControlsWork(matrix.Controls);
          break;
      }
    }

    private void GroupsWork(List<Group> groups)
    {
      foreach (var group in groups)
      {
        var controlAct = _control.FindAct(group.Controls);
        if (controlAct == null) continue;
        switch (controlAct.Data)
        {
          case Act.Add:
            _work.GroupsToAdd.Add(group);
            break;
          case Act.Remove:
            _work.GroupsToRemove.Add(group);
            break;
          case Act.Update:
            UnitsWork(group.Units);
            ControlsWork(group.Controls);
            break;
        }
      }
    }

    private void UnitsWork(List<Unit> units)
    {
      foreach (var Unit in units)
      {
        var controlAct = _control.FindAct(Unit.Controls);
        if (controlAct == null) continue;
        {
          switch (controlAct.Data)
          {
            case Act.Add:
              _work.UnitsToAdd.Add(Unit);
              break;
            case Act.Remove:
              _work.UnitsToRemove.Add(Unit);
              break;
            case Act.Update:
              ControlsWork(Unit.Controls);
              break;
          }
        }
      }
    }

    private void ControlsWork(List<Control> controls)
    {
      foreach (var control in controls)
      {
        if (control.Id != Guid.Empty)
        {
          if (string.IsNullOrEmpty(control.Indicator) && string.IsNullOrEmpty(control.Data))
          {
            _work.ControlsToRemove.Add(control);
          }
          else
          {
            _work.ControlsToUpdate.Add(control);
          }
        }
        else
        {
          if (!string.IsNullOrEmpty(control.Indicator) || !string.IsNullOrEmpty(control.Data))
          {
            _work.ControlsToAdd.Add(control);
          }
        }
      }
    }
  }

  public class Work
  {
    public Matrix? MatrixToAdd;

    public List<Group> GroupsToAdd = [];
    public List<Group> GroupsToRemove = [];

    public List<Unit> UnitsToAdd = [];
    public List<Unit> UnitsToRemove = [];

    public List<Control> ControlsToAdd = [];
    public List<Control> ControlsToUpdate = [];
    public List<Control> ControlsToRemove = [];
  }

  public interface IWorkService
  {
    Work OnInit(MatrixDto matrixDto);
  }
}
