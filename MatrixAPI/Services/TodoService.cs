using MatrixAPI.Models;
using System.Collections.Generic;

namespace MatrixAPI.Services
{
  public class TodoService(IMapService map, IControlService control) : ITodoService
  {
    private readonly IMapService _map = map;
    private readonly IControlService _control = control;

    private readonly Todo _todo = new();

    public Todo Matrix(MatrixDto matrixDto)
    {
      var matrix = _map.ToMatrix(matrixDto);
      GroupsTodo(matrix.Groups);
      ControlsTodo(matrix.Controls);
      return _todo;
    }

    private void GroupsTodo(List<Group> groups)
    {
      foreach (var group in groups)
      {
        var controlAct = _control.FindAct(group.Controls);
        if (controlAct == null) continue;

        switch (controlAct.Data)
        {
          case Act.Add:
            _todo.GroupsToAdd.Add(group);
            break;
          case Act.Remove:
            _todo.GroupsToRemove.Add(group);
            break;
          case Act.Update:
            UnitsTodo(group.Units);
            ControlsTodo(group.Controls);
            break;
        }
      }
    }

    private void UnitsTodo(List<Unit> units)
    {
      foreach (var Unit in units)
      {
        var controlAct = _control.FindAct(Unit.Controls);
        if (controlAct == null) continue;
        {
          switch (controlAct.Data)
          {
            case Act.Add:
              _todo.UnitsToAdd.Add(Unit);
              break;
            case Act.Remove:
              _todo.UnitsToRemove.Add(Unit);
              break;
            case Act.Update:
              ControlsTodo(Unit.Controls);
              break;
          }
        }
      }
    }

    private void ControlsTodo(List<Control> controls)
    {
      foreach (var control in controls)
      {
        if (control.Id == null)
        {
          if (!string.IsNullOrEmpty(control.Indicator) || !string.IsNullOrEmpty(control.Data))
          {
            _todo.ControlsToAdd.Add(control);
          }
        }
        else
        {
          if (string.IsNullOrEmpty(control.Indicator) && string.IsNullOrEmpty(control.Data))
          {
            _todo.ControlsToRemove.Add(control);
          }
          else
          {
            _todo.ControlsToUpdate.Add(control);
          }
        }
      }
    }
  }

  public class Todo
  {
    public List<Group> GroupsToAdd = [];
    public List<Group> GroupsToRemove = [];

    public List<Unit> UnitsToAdd = [];
    public List<Unit> UnitsToRemove = [];

    public List<Control> ControlsToAdd = [];
    public List<Control> ControlsToUpdate = [];
    public List<Control> ControlsToRemove = [];
  }

  public interface ITodoService
  {
    public Todo Matrix(MatrixDto matrixDto);
  }
}
