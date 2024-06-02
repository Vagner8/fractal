using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public class ControlService : IControlService
  {
    public C? Find<C>(List<C> controls, string prop, string indicator)
    {
      var info = typeof(C).GetProperty(prop);
      if (info == null) return default;
      return controls.FirstOrDefault(c => info.GetValue(c)?.ToString() == indicator);
    }

    public C? FindAct<C>(List<C> controls)
    {
      return Find(controls, ControlKey.Indicator, Indicator.Act);
    }

    public Control MatrixControl(string indicator, string data, Guid matrixId)
    {
      return new Control
      {
        Id = Guid.NewGuid(),
        Indicator = indicator,
        Data = data,
        MatrixId = matrixId,
      };
    }

    public Control GroupControl(string indicator, string data, Guid groupId)
    {
      return new Control
      {
        Id = Guid.NewGuid(),
        Indicator = indicator,
        Data = data,
        GroupId = groupId,
      };
    }

    public Control UnitControl(string indicator, string data, Guid unitId)
    {
      return new Control
      {
        Id = Guid.NewGuid(),
        Indicator = indicator,
        Data = data,
        UnitId = unitId,
      };
    }
  }

  public interface IControlService
  {
    C? Find<C>(List<C> controls, string prop, string indicator);
    C? FindAct<C>(List<C> controls);
    Control MatrixControl(string indicator, string data, Guid matrixId);
    Control GroupControl(string indicator, string data, Guid groupId);
    Control UnitControl(string indicator, string data, Guid unitId);
  }
}