namespace FractalAPI.Dto
{
  public class FractalDto : BaseDto
  {
    public Dictionary<string, FractalDto>? Children { get; set; }
    public Dictionary<string, ControlDto>? Controls { get; set; }
    public Dictionary<string, ControlDto>? ChildrenControls { get; set; }
  }
}
