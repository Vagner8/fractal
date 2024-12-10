namespace FractalAPI.Models
{
  public class ControlDto
  {
    public Guid? Id { get; set; }
    public Guid? ParentId { get; set; }
    public object Data { get; set; } = string.Empty;
    public string Indicator { get; set; } = string.Empty;
  }
}