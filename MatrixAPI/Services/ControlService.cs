using MatrixAPI.Models;

namespace MatrixAPI.Services
{
  public class ControlService : IControlService
  {
    public C? Find<C>(List<C> controls, string prop, string value)
    {
      var info = typeof(C).GetProperty(prop);
      if (info == null) return default;
      return controls.FirstOrDefault(c => info.GetValue(c)?.ToString() == value);
    }

    public C? FindAct<C>(List<C> controls)
    {
      return Find(controls, ControlProp.Name, ControlData.Act);
    }
  }

  public interface IControlService
  {
    C? Find<C>(List<C> controls, string prop, string value);
    C? FindAct<C>(List<C> controls);
  }
}