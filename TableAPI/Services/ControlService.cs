using MatrixAPI.Models;

namespace MatrixAPI.Services
{
    public class ControlService(IMapService map) : IControlService
    {
        private readonly IMapService _map = map;

        public void Add(ICollection<Control> controls, ControlDto controlDto)
        {
            controls.Add(_map.ToControl(controlDto));
        }

        public void Update(Control control, ControlDto controlDto)
        {
            control.Name = controlDto.Name;
            control.Value = controlDto.Value;
            control.Version = Guid.NewGuid();
        }

        public void Remove(ICollection<Control> controls, Control control)
        {
            controls.Remove(control);
        }

        public Control GetControl(ICollection<Control> controls, Guid? id)
        {
            return controls.FirstOrDefault(c => c.Id == id) ?? throw new Exception($"Find control: {id}");
        }
    }

    public interface IControlService
    {
        public void Add(ICollection<Control> controls, ControlDto controlDto);
        public void Update(Control control, ControlDto controlDto);
        public void Remove(ICollection<Control> controls, Control control);
        public Control GetControl(ICollection<Control> controls, Guid? id);
    }
}
